﻿namespace db

module Database =

    open System.Data
    open System.Data.Common
    open System.Configuration

    //let connectionString = "Host=localhost;Username=paulm;Password=reddingo;Database=golangtest"
    //let conn = new Npgsql.NpgsqlConnection(connectionString)
    let connectionStringName = "golangtest"
    let mutable connection : DbConnection = null

    type Subject = {Id:int; Name:string}

    let findconnectionstringbyname (name: string) (settings: ConnectionStringSettingsCollection) =
        let fooseq = Seq.cast<ConnectionStringSettings> settings
        let foo = Seq.find (fun (i: ConnectionStringSettings) -> i.Name.Equals(name)) fooseq
        match foo with
        | null -> None
        | _ -> Some(foo.ConnectionString)
       
        (*
        for setting in settings do
            if setting.Name.Equals(name) then
                let connectString = setting.ConnectionString
                ""
            else
                ""
        *)

    let getconnectionstring (name: string) =
        let settings = ConfigurationManager.ConnectionStrings
        match settings with 
        | null -> None
        | _ -> Some(findconnectionstringbyname name settings)



    let opendatabase () =
        let constring = getconnectionstring connectionStringName
        let connectionstring = 
            match constring with 
            | Some(constring) -> Option.get(constring)
            | None -> ""
        let factory: DbProviderFactory = DbProviderFactories.GetFactory("Npgsql");
        connection <- factory.CreateConnection()
        connection.ConnectionString <- connectionstring
        connection.Open()
      //  conn.Open()
        printfn "database opened"

    let runinsertcommand () =
        let query = "insert into script (name, engine, body) values ('scala', 'scala', 'def func = fred')"
        use com = connection.CreateCommand()
        com.CommandText <- query
        com.CommandType <- CommandType.Text
        let rows = com.ExecuteNonQuery()
        printfn "Rows inserted: %i" rows

    let runtestquery () =
        let query = "select id, name, engine, body from script"
        use com = connection.CreateCommand()
        com.CommandText <- query
        com.CommandType <- CommandType.Text
        use reader = com.ExecuteReader()
        while reader.Read() do
            printfn "id:%i name:%s" (reader.GetInt64(0)) (reader.GetString(1))
        
        (*
        use cmd = new Npgsql.NpgsqlCommand("select id, name, engine, body from script", conn)
        use reader = cmd.ExecuteReader()
        while reader.Read() do
            printfn "id:%i name:%s" (reader.GetInt64(0)) (reader.GetString(1))
        *)

    let closedatabase () =
        connection.Close()


    let insertsubject bookid name =
        let query = "insert into subject (bookid, name) values (@BookId, @Name); select currval('subject_id_seq');"
        use com = connection.CreateCommand()
        com.CommandText <- query
        com.CommandType <- CommandType.Text

        let bookidparam = com.CreateParameter()
        bookidparam.ParameterName <- "@BookId"
        bookidparam.DbType <- DbType.Int32
        bookidparam.Value <- bookid
        com.Parameters.Add(bookidparam) |> ignore

        let param = com.CreateParameter()
        param.ParameterName <- "@Name"
        param.Value <- name
        param.DbType <- DbType.String
        com.Parameters.Add(param) |> ignore


        let id = com.ExecuteScalar()
        let insertedId = id :?> int64
        printfn "id inserted: %A" insertedId
        insertedId

    let insertchapter subjectid name =
        let query = "insert into chapter (subjectid, name) values (@SubjectId, @Name); select currval('chapter_id_seq');"   
        use com = connection.CreateCommand()
        com.CommandText <- query
        com.CommandType <- CommandType.Text

        let idparam = com.CreateParameter()
        idparam.ParameterName <- "@SubjectId"
        idparam.DbType <- DbType.Int32
        idparam.Value <- subjectid
        com.Parameters.Add(idparam) |> ignore

        let param = com.CreateParameter()
        param.ParameterName <- "@Name"
        param.Value <- name
        param.DbType <- DbType.String
        com.Parameters.Add(param) |> ignore

        let id = com.ExecuteScalar()
        id :?> int64
