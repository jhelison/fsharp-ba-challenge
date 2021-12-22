namespace MarketData

open Microsoft.AspNetCore.Http
open Saturn
open Persistence
open Shared

module Controller =


    let indexAction (ctx : HttpContext) =
        task {
            let unitOfWork = new UnityOfWork() :> Core.IUnityOfWork
            let marketData = unitOfWork.MarketData.All
            return marketData
        }

    let showAction (ctx: HttpContext) (id : string) =
        task {
            let unitOfWork = new UnityOfWork() :> Core.IUnityOfWork
            let marketData = unitOfWork.MarketData.Get (int id)
            return marketData
        }

    let createAction (ctx: HttpContext) =
        task {
            let unitOfWork = new UnityOfWork() :> Core.IUnityOfWork

            let input = (Controller.getModel<MarketData> ctx).Result

            let marketData = unitOfWork.MarketData.Get input.id
            if not(obj.ReferenceEquals(marketData, null)) then
                return Response.conflict
            else

            unitOfWork.MarketData.Add input |> ignore
            unitOfWork.Save
            return Response.ok
        }

    let updateAction (ctx: HttpContext) (id : string) =
        task {
            // let unitOfWork = new UnityOfWork() :> Core.IUnityOfWork

            // let marketData = unitOfWork.MarketData.Get (int id)
            // if obj.ReferenceEquals(marketData, null) then
            //     return Response.notFound
            // else

            // let input = (Controller.getModel<MarketData> ctx).Result

            // return Response.ok
            return "updateAction"
        }

    let deleteAction (ctx: HttpContext) (id : string) =
        task {
            let unitOfWork = new UnityOfWork() :> Core.IUnityOfWork

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

