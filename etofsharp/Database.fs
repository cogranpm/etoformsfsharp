module Database

open System.Data

let connectionString = "Host=localhost;Username=paulm;Password=reddingo;Database=golangtest"

let opendatabase =
    use conn = new Npgsql.NpgsqlConnection(connectionString)
    conn.Open()
    conn.Close()