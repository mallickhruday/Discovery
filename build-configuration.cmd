@echo off

%FAKE% %NYX% "target=clean" -st
%FAKE% %NYX% "target=RestoreNugetPackages" -st

IF NOT [%1]==[] (set RELEASE_NUGETKEY="%1")
IF NOT [%2]==[] (set RELEASE_TARGETSOURCE="%2")

SET SUMMARY="Discovery Abstractions"
SET DESCRIPTION="Discovery Abstractions"

SET SUMMARY_PHRASEAPP="Consul Discovery"
SET DESCRIPTION_PHRASEAPP="Consul Discovery"

%FAKE% %NYX% appName=Discovery                       appSummary=%SUMMARY% appDescription=%DESCRIPTION% nugetserver=%NUGET_SOURCE_DEV_PUSH% nugetkey=%RELEASE_NUGETKEY%
%FAKE% %NYX% appName=Discovery.Consul             appSummary=%SUMMARY_PHRASEAPP% appDescription=%DESCRIPTION_PHRASEAPP% nugetserver=%NUGET_SOURCE_DEV_PUSH% nugetkey=%RELEASE_NUGETKEY%