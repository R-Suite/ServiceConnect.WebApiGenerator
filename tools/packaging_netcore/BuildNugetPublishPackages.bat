SET OUTDIR=C:\GIT\ServiceConnect.WebApiGenerator\src\

@ECHO === === === === === === === ===

@ECHO ===NUGET Publishing ....

del *.nupkg

:: comment

dotnet pack "%OUTDIR%ServiceConnect.WebApiGenerator\ServiceConnect.WebApiGenerator.csproj" -c Release /p:PackageVersion=1.0.1

nuget push %OUTDIR%ServiceConnect.WebApiGenerator\bin\Release\ServiceConnect.WebApiGenerator.1.0.1.nupkg -Source https://www.nuget.org/api/v2/package

@ECHO === === === === === === === ===

PAUSE
