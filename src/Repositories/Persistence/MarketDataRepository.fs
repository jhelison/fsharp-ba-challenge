namespace Persistence

open Microsoft.EntityFrameworkCore
open Models
open Shared
open System

type MarketDataRepository (context : DataContext) =
    inherit GenericRepository<MarketData> (context)

    interface Core.IMarketDataRepository with
        member this.Filter (filters: MarketDataFilters) =
            filters |> Console.WriteLine |> ignore

            let mutable baseQuery =
                query {
                    for marketdata in context.marketdata do
                        select marketdata
                }

            match filters.minPrice with
                | Some s ->
                    baseQuery <- query {
                        for marketdata in baseQuery do
                            where (marketdata.price >= s)
                    }
                | None -> None |> ignore
            match filters.maxPrice with
                | Some s ->
                    baseQuery <- query {
                        for marketdata in baseQuery do
                            where (marketdata.price <= s)
                    }
                | None -> None |> ignore

            match filters.minQuantity with
                | Some s ->
                    baseQuery <- query {
                        for marketdata in baseQuery do
                            where (marketdata.quantity >= s)
                    }
                | None -> None |> ignore
            match filters.maxQuantity with
                | Some s ->
                    baseQuery <- query {
                        for marketdata in baseQuery do
                            where (marketdata.quantity <= s)
                    }
                | None -> None |> ignore

            match filters.startDate with
                | Some s ->
                    baseQuery <- query {
                        for marketdata in baseQuery do
                            where (marketdata.date >= s)
                    }
                | None -> None |> ignore
            match filters.endDate with
                | Some s ->
                    baseQuery <- query {
                        for marketdata in baseQuery do
                            where (marketdata.date <= s)
                    }
                | None -> None |> ignore

            let getEntity() =
                async {
                    return! baseQuery.ToArrayAsync()
                        |> Async.AwaitTask
                }
            let entity = getEntity() |> Async.RunSynchronously
            entity
