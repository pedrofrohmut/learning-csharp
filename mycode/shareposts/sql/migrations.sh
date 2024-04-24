#! /usr/bin/bash

case $1 in
    "1")
        docker cp ./sql/0001_create_database.sql shareposts-db-1:/
        docker exec -t shareposts-db-1 psql -U postgres -f /0001_create_database.sql
        ;;

    "2")
        docker cp ./sql/0002_create_initial_tables.sql shareposts-db-1:/
        docker exec -t shareposts-db-1 psql -U postgres -f /0002_create_initial_tables.sql shareposts_db
        ;;

    "clean")
        docker cp ./sql/clean_db.sql shareposts-db-1:/
        docker exec -t shareposts-db-1 psql -U postgres -f /clean_db.sql
        docker exec -t shareposts-db-1 rm /0001_create_database.sql
        docker exec -t shareposts-db-1 rm /0002_create_initial_tables.sql
        docker exec -t shareposts-db-1 rm /clean_db.sql
        ;;

    *)
        echo "No options provided. Doing nothing."
        ;;
esac
