namespace Persistence

open Shared

type UnityOfWork() =
    let context = new DataContext()

    interface Core.IUnityOfWork with
        member this.MarketData
            with get() = new GenericRepository<MarketData>(context) :> Core.IRepository<MarketData>

        member this.Save =
            context.SaveChanges() |> ignore
