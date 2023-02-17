#!/usr/bin/env bash

name="$1"

if [[-z "$name"]]; then
    echo "Please input your migration name"
    read name
fi

echo "Starting creation of migrations..."

echo "creating SQLite migration"
dotnet ef migrations add $name --context SqliteContext --output-dir "./Migrations/SQLITE/"
echo "SQLite migration created"

echo "creating MSSQL migration"
dotnet ef migrations add $name --context MssqlContext --output-dir "./Migrations/MSSQL/"
echo "MSSQL migration created"

echo "creating MySql migration"
dotnet ef migrations add $name --context MysqlContext --output-dir "./Migrations/MySQL/"
echo "MySql migration created"

echo "creating Postgres migration"
dotnet ef migrations add $name --context PostgresContext --output-dir "./Migrations/Postgres/"
echo "Postgres migration created"

echo "Migrations finished!"