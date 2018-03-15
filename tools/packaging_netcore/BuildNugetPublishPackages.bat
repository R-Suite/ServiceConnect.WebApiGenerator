SET OUTDIR=C:\GIT\ServiceConnect.WebApiGenerator\src\

@ECHO === === === === === === === ===

@ECHO ===NUGET Publishing ....

del *.nupkg

:: comment

dotnet pack "%OUTDIR%ServiceConnect.WebApiGenerator\ServiceConnect.WebApiGenerator.csproj" -c Release /p:PackageVersion=1.0.5
::NuGet pack "%OUTDIR%ServiceConnect.WebApiGenerator\ServiceConnect.WebApiGenerator.nuspec"

nuget push %OUTDIR%ServiceConnect.WebApiGenerator\bin\Release\ServiceConnect.WebApiGenerator.1.0.5.nupkg -Source https://api.nuget.org/v3/index.json

@ECHO === === === === === === === ===

PAUSE
