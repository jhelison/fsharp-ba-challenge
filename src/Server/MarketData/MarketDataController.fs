namespace MarketData

open Microsoft.AspNetCore.Http
open Saturn
open Persistence
open Shared

module Controller =

    let unitOfWork = new UnityOfWork() :> Core.IUnityOfWork

    let indexAction (ctx : HttpContext) =
        task {
            let marketData = unitOfWork.MarketData.All
            return marketData
        }

    let showAction (ctx: HttpContext) (id : string) =
        task {
            let marketData = unitOfWork.MarketData.Get (int id)
            if obj.ReferenceEquals(marketData, null) then
                return! Response.notFound ctx null
            else
            return! Response.ok ctx marketData
        }

    let createAction (ctx: HttpContext) =
        task {
            let input = (Controller.getModel<MarketData> ctx).Result

            let marketData = unitOfWork.MarketData.Get input.id
            if not(obj.ReferenceEquals(marketData, null)) then
                "IF" |> System.Console.WriteLine |> ignore
                return! Response.conflict ctx null
            else

            unitOfWork.MarketData.Add input |> ignore
            unitOfWork.Save
            return! Response.ok ctx null
        }

    let updateAction (ctx: HttpContext) (id : string) =
        task {
            //This action is buggy, fix after
            let input = (Controller.getModel<MarketData> ctx).Result

            unitOfWork.MarketData.Update input |> ignore
            unitOfWork.Save |> ignore

            return Response.ok
        }

    let deleteAction (ctx: HttpContext) (id : string) =
        task {
            let marketData = unitOfWork.MarketData.Get (int id)
            if obj.ReferenceEquals(marketData, null) then
                return Response.notFound
            else

            unitOfWork.MarketData.Delete (int id) |> ignore
            unitOfWork.Save
            return Response.ok
        }

    let resource = controller {
        index indexAction
        show showAction
        create createAction
        update updateAction
        delete deleteAction
    }

