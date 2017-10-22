#!/bin/bash
cd /pipeline/source/app/publish
dotnet hwapp.dll --server.urls=http://*:8080
