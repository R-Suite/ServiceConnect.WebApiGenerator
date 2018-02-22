using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ServiceConnect.WebApiGenerator.Filters
{
    public class HandlerEndpointsFilter : IDocumentFilter
    {
        private PathItem HandlerPathItem(int i)
        {
            var x = new PathItem();
            x.Post = new Operation
            {
                Tags = new[] {"MyMessageHandler"},
                OperationId = "Fake_Get" + i,
                Consumes = new[] {"application/json"},
                Produces = new[] {"application/json", "text/json", "application/xml", "text/xml"},
                Parameters = new List<IParameter>
                {
                    new BodyParameter
                    {
                        Name = "message",
                        @In = "body",
                        Required = true,
                        Description = "message",
                        Schema = new Schema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, Schema>
                            {
                                {"correlationId", new Schema {Type = "string"}},
                                {"name", new Schema {Type = "string"}}
                            }
                        }
                    }
                }
            };
            x.Post.Responses = new Dictionary<string, Response>();
            x.Post.Responses.Add("200", new Response { Description = "OK", Schema = new Schema { Type = "string" } });
            return x;
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths.Add("/myswagger/mymessagehandler", HandlerPathItem(1));
        }
    }
}
