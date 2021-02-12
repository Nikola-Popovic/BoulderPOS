# This script is not used at the moment but could be if Postgres is dockerized.
until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"