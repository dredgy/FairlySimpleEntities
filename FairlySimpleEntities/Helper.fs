module FairlySimpleEntities.Helper

open System.Collections.Generic
open System.Linq

let RunSynchronously task =
    task
    |> Async.AwaitTask
    |> Async.RunSynchronously
    
let EnumerableToArray (enumerable: IEnumerable<'T>) =  enumerable.ToArray()  