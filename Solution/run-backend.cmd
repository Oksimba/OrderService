@echo off
dotnet build --source ..\OrderAPI\ --configuration Release 
dotnet run --project .\OrderAPI\ -c Release