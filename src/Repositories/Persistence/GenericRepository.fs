namespace Persistence

open Microsoft.EntityFrameworkCore

type GenericRepository<'T when 'T : not struct> () =
    let context = new DataContext()

    interface Core.IRepository<'T> with
        member this.All =
            let getEntity() =
                async {
                    return! context.Set<'T>().ToArrayAsync()
                        |> Async.AwaitTask
                }
            let entity = getEntity() |> Async.RunSynchronously
            entity

        member this.Get id =
            context.Set<'T>().Find id

        member this.Add entity : 'T =
            context.Set<'T>().Add entity
                |> ignore
            entity

        member this.Delete id =
            let entity = context.Set<'T>().Find id
            context.Remove entity |> ignore

        member this.Save =
            context.SaveChanges() |> ignore
