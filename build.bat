@echo off
cls

REM Set the paths to the project folders
set "coreProjectPath=Core"
set "editorProjectPath=Editor"
set "buildFolderPath=Build"

REM Create the "Build" folder if it doesn't exist
if not exist "%buildFolderPath%" (
    mkdir "%buildFolderPath%"
)

REM Build the Core project
echo Building Core...
cd "%coreProjectPath%"
dotnet build -c Release -o "../%buildFolderPath%/Core"
cd ..

REM Change title for Editor build
title Building Editor...

REM Build the Editor project
echo Building Editor...
cd "%editorProjectPath%"
dotnet build -c Release -o "../%buildFolderPath%/Editor"
cd ..

REM Change title back to default
title Build Complete

echo Build complete. Output is in the "%buildFolderPath%" folder.
pause
