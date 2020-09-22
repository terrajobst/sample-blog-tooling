@echo off
setlocal
set PROJECT_FILE=%~dp0src\blog-validate\blog-validate.csproj
dotnet run --project %PROJECT_FILE% -- %~dp0