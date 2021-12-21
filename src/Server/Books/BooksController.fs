namespace Books

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.ContextInsensitive
open Saturn

module Controller =

  let indexAction (ctx : HttpContext) =
    task {
        return "indexAction"
    }

  let showAction (ctx: HttpContext) (id : string) =
    task {
        return "showAction"
    }

  let createAction (ctx: HttpContext) =
    task {
        return "createAction"
    }

  let updateAction (ctx: HttpContext) (id : string) =
    task {
        return "updateAction"
    }

  let deleteAction (ctx: HttpContext) (id : string) =
    task {
        return "deleteAction"
    }

  let resource = controller {
    index indexAction
    show showAction
    create createAction
    update updateAction
    delete deleteAction
  }

