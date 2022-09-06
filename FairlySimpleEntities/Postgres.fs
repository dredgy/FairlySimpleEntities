﻿module FairlySimpleEntities.Postgres

open System.Data
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open Helper

let select<'x> (query: SelectQuery) (connection: IDbConnection) =
    query
        |> connection.SelectAsync<'x>
        |> RunSynchronously
        |> EnumerableToArray              