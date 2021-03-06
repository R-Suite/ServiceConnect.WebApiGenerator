﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using ServiceConnect.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ServiceConnect.WebApiGenerator.Filters
{
    public class HandlerEndpointsFilter : IDocumentFilter
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(HandlerEndpointsFilter));

        private PathItem HandlerPathItem(HandlerReference handlerRef)
        {
            var messageType = Type.GetType(handlerRef.MessageType.AssemblyQualifiedName);
            var routingKeys = handlerRef.RoutingKeys;

            if (null == messageType)
            {
                var err = string.Format("Unable to load the type {0}", handlerRef.MessageType.AssemblyQualifiedName);
                Logger.Error(err);
                throw new Exception(err);
            }

            Logger.DebugFormat("messageType.Name: {0}.", messageType.Name);

            var msgProperties = new Dictionary<string, Schema>();
            var props = messageType.GetProperties();
            foreach (var prop in props)
            {
                string typeName = prop.PropertyType.Name.ToLower();
                if (typeName == "guid")
                {
                    typeName = "string";
                }

                msgProperties.Add(prop.Name, new Schema {Type = typeName });
            }

            var x = new PathItem();
            x.Post = new Operation
            {
                Tags = new[] { handlerRef.HandlerType.Name },
                OperationId = handlerRef.HandlerType.Name,
                Consumes = new[] {"application/json"},
                Produces = new[] {"application/json", "text/json", "application/xml", "text/xml"},
                Parameters = new List<IParameter>
                {
                    new BodyParameter
                    {
                        Name = "message",
                        @In = "body",
                        Required = true,
                        Description = messageType.Name,
                        Schema = new Schema
                        {
                            Type = "object",
                            Properties = msgProperties
                        }
                    },
                    new NonBodyParameter
                    {
                        Name = "msg-full-type-name",
                        @In = "header",
                        Required = true,
                        Default = messageType.AssemblyQualifiedName
                    }
                }
            };

            if (null != routingKeys && routingKeys.Any())
            {
                var rkStr = String.Join(",", routingKeys.ToArray());
                x.Post.Parameters.Add(new NonBodyParameter
                {
                    Name = "routing-key",
                    @In = "header",
                    Required = true,
                    Default= routingKeys.First(),
                    Description = $"warning: message will be published to every \"{messageType.Name}\" handler decorated with the routing key supplied. (possible value(s): {rkStr})"
                });
            }

            if (Generator.Bus.Configuration.BeforeConsumingFilters.Any(f=>f.Name == "TokenDecryptFilter"))
            {
                x.Post.Parameters.Add(new NonBodyParameter
                {
                    Name = "token",
                    @In = "header",
                    Required = true,
                    Default = string.Empty
                });
            }

            x.Post.Responses = new Dictionary<string, Response>();
            x.Post.Responses.Add("200", new Response { Description = "OK", Schema = new Schema { Type = "string" } });
            return x;
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            // add path for each handler
            IEnumerable<HandlerReference> handlerRefs = Generator.Bus.Configuration.GetContainer().GetHandlerTypes();
            foreach (var handlerRef in handlerRefs)
            {
                Logger.DebugFormat("adding path for: {0}.", handlerRef.HandlerType.Name);
                swaggerDoc.Paths.Add("/api/handlers/" + handlerRef.HandlerType.Name, HandlerPathItem(handlerRef));
            }

            // remove the generic path
            var pathsToRemove = swaggerDoc.Paths.First(p => p.Key.Contains("/handlers/{handler}"));
            swaggerDoc.Paths.Remove(pathsToRemove.Key);
        }
    }
}
