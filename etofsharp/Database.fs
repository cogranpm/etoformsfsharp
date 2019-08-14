namespace db

module Database =

    open System.Data

    let connectionString = "Host=localhost;Username=paulm;Password=reddingo;Database=golangtest"

    let conn = new Npgsql.NpgsqlConnection(connectionString)

    let opendatabase =
        conn.Open()
        printfn "database opened"

    let runtestquery =
        use cmd = new Npgsql.NpgsqlCommand("select id, name, engine, body from script", conn)
        use reader = cmd.ExecuteReader()
        while reader.Read() do
            printfn "id:%i name:%s" (reader.GetInt64(0)) (reader.GetString(1))


    let closedatabase =
        conn.Close()
        printfn "database closed"