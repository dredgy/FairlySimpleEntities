module FairlySimpleEntities

open FairlySimpleEntities.Types
open Dapper.FSharp

let GetPostgresConnectionString dbConfig =
    $"Server={dbConfig.DatabaseHost};Port={dbConfig.DatabasePort};User Id={dbConfig.DatabaseUsername};Password={dbConfig.DatabasePassword};Database={dbConfig.DatabaseName};Include Error Detail=true" 

let GetMySQLConnectionString dbConfig =
    $"Server={dbConfig.DatabaseHost};Port={dbConfig.DatabasePort};User Id={dbConfig.DatabaseUsername};Password={dbConfig.DatabasePassword};Database={dbConfig.DatabaseName};Include Error Detail=true" 

let GetMSSQLConnectionString dbConfig =
    $"Server={dbConfig.DatabaseHost};Port={dbConfig.DatabasePort};User Id={dbConfig.DatabaseUsername};Password={dbConfig.DatabasePassword};Database={dbConfig.DatabaseName};Include Error Detail=true"

let OpenDatabaseConnection dbConfig =
    let connectionStringFunction =
        match dbConfig.DatabaseEngine with
            | MySQL -> GetMySQLConnectionString
            | Postgres -> GetPostgresConnectionString
        
    let connectionString = connectionStringFunction dbConfig
    
    match dbConfig.DatabaseEngine with
        | Postgres -> PostgresConnection (new Npgsql.NpgsqlConnection(connectionString))
        | _ -> MySQLConnection (new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        
        
let getSelectFunc<'x> connection =
    match connection with
        | PostgresConnection _ -> FairlySimpleEntities.Postgres.select<'x>
        | MySQLConnection _ -> FairlySimpleEntities.MySQL.select<'x>
    
let inline GetAll< 'x when  'x: (member table: string)> connection =
    let selectFunc = getSelectFunc<'x> connection
    let defaultType = Unchecked.defaultof<'x>
    let tableName = typeof<'x>.GetProperty("table").GetValue(defaultType) |> string
    select {
        table tableName
    }
    |> selectFunc