namespace Persistence

open Microsoft.EntityFrameworkCore
open Models
open Shared

type MarketDataRepository (context : DataContext) =
    inherit GenericRepository<MarketData> (context)

    interface Core.IMarketDataRepository with
        member this.Filter (filters: MarketDataFilters) =
            let getEntity() =
                async {
                    return! context.Set<'T>().ToArrayAsync()
                        |> Async.AwaitTask
                }
            let entity = getEntity() |> Async.RunSynchronously
            entity
