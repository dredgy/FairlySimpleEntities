module FairlySimpleEntities

open FairlySimpleEntities.Types

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
        
let GetAll< ^x when  ^x: (member table: string)> connection =
    "Select * From "