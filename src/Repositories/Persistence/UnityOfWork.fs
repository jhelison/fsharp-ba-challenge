namespace Persistence

open Models

type UnityOfWork() =
    let context = new DataContext()

    interface Core.IUnityOfWork with
        member this.MarketData
            with get() = new MarketDataRepository(context) :> Core.IMarketDataRepository

        member this.Save =
            context.SaveChanges() |> ignore
