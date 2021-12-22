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
            //To do
            // Add valitation if the id exists
            let input = (Controller.getModel<MarketData> ctx).Result

            let marketData = unitOfWork.MarketData.Get input.id
            if not(obj.ReferenceEquals(marketData, null)) then
                return Response.conflict ctx "Conflito"
            else

            unitOfWork.MarketData.Add input |> ignore
            unitOfWork.Save
            return Response.ok ctx "created with sucess"
        }

    let updateAction (ctx: HttpContext) (id : string) =
        task {
        return "updateAction"
        }

    let deleteAction (ctx: HttpContext) (id : string) =
        task {
            let unitOfWork = new UnityOfWork() :> Core.IUnityOfWork

            let marketData = unitOfWork.MarketData.Get (int id)
            if obj.ReferenceEquals(marketData, null) then
                return Response.notFound ctx "Not found"
            else

            unitOfWork.MarketData.Delete (int id) |> ignore
            unitOfWork.Save
            return Response.ok ctx "deleted with sucess"
        }

    let resource = controller {
        index indexAction
        show showAction
        create createAction
        update updateAction
        delete deleteAction
    }

