@echo off
cls

set "pathCore=Core"
set "pathEditor=Editor"
set "pathBuild=Build"
set "pathDeploy=C:\Program Files (x86)\Steam\steamapps\common\ROUNDS\BepInEx\plugins"

if not exist "%pathBuild%" (
    mkdir "%pathBuild%"
)

title Building Core...

echo Building Core...
cd "%pathCore%"
dotnet build -c Release -o "../%pathBuild%/Core"
cd ..

title Building Editor...

echo Building Editor...
cd "%pathEditor%"
dotnet build -c Release -o "../%pathBuild%/Editor"
cd ..

title Deploying...

echo Moving built dll files to deploy location...
copy "%pathBuild%\Core\MapImageObjects.dll" "%pathDeploy%"
copy "%pathBuild%\Editor\MapImageObjectsEditor.dll" "%pathDeploy%"

title Build Complete

echo Build complete. Output is in the "%pathBuild%" folder. DLL files deployed to: "%pathDeploy%"
pause
