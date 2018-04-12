@echo off

@powershell -File .nyx\build.ps1 '--appname=Discovery' '--nugetPackageName=Discovery'
@powershell -File .nyx\build.ps1 '--appname=Discovery.Consul' '--nugetPackageName=Discovery.Consul'