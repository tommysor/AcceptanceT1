#!/bin/bash

pushd ./src
dotnet run --project DealWith.AppHost/ &
APP_PID=$!
popd

# Wait for the app to start
sleep 15

pushd ./spesifications
SPESIFICATIONS_BASEADDRESS=http://localhost:5041 dotnet test
popd

kill $APP_PID
