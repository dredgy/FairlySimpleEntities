module FairlySimpleEntities.Types

type DatabaseEngine = MySQL | Postgres
type DatabaseConfig = {
    DatabaseEngine: DatabaseEngine
    DatabaseName: string
    DatabaseUsername: string
    DatabasePassword: string
    DatabaseHost: string
    DatabasePort: int
}

type DatabaseConnection =
    | MySQLConnection of MySql.Data.MySqlClient.MySqlConnection
    | PostgresConnection of Npgsql.NpgsqlConnection 