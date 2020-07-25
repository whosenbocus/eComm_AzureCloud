#!/bin/bash
export PATH="$PATH:/root/.dotnet/tools"
set -e
run_cmd="dotnet run"
run_cmd_ef="dotnet tool install --global dotnet-ef"

dotnet tool install --global dotnet-ef
until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"
exec $run_cmd