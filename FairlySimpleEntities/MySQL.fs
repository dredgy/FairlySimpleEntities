module FairlySimpleEntities.MySQL

open FairlySimpleEntities
open Helper
open System.Data
open Dapper.FSharp.MySQL

let select<'x> query (connection: IDbConnection) =
    query
        |> connection.SelectAsync<'x>
        |> RunSynchronously
        |> EnumerableToArray