namespace Persistence

open Shared

type UnityOfWork() =
    let context = new DataContext()

    interface Core.IUnityOfWork with
        member this.MarketData
            with get() = new GenericRepository<MarketData>() :> Core.IRepository<MarketData>

        member this.Save =
            context.SaveChanges() |> ignore
