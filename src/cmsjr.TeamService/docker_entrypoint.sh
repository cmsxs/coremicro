#!/bin/bash
cd /pipeline/source/app/publish
dotnet cmsjr.TeamService.dll --server.urls=http://*:8080
