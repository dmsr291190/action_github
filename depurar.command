#!/bin/bash
cd "$(dirname "$0")"
# Ejecutar en modo debug usando dotnet watch
ASPNETCORE_ENVIRONMENT=Development dotnet watch --project Minem.Tupa/Minem.Tupa.Api.csproj run
