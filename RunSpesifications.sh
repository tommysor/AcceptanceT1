#!/bin/bash

dotnet run --project src/DealWith.AppHost/ &
APP_PID=$!

# Wait for the app to start
sleep 15

pushd ./spesifications
SPESIFICATIONS_BASEADDRESS=http://localhost:5041 dotnet test
popd

kill $APP_PID
