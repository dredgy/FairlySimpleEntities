module FairlySimpleEntities.Tests

open FairlySimpleEntities
open FairlySimpleEntities.Types

let DatabaseConfig : DatabaseConfig = {
    DatabaseEngine = MySQL
    DatabaseName = "ds"
    DatabaseUsername = ""
    DatabasePassword = ""
    DatabaseHost = "localhost"
    DatabasePort = 3306
}

type user = {
    id: int
    username: string
    password: string
} with member this.table = "users"

let connectToDatabase () = OpenDatabaseConnection DatabaseConfig
