namespace Persistence

open Microsoft.EntityFrameworkCore

open Shared

type DataContext() =
    inherit DbContext()

    [<DefaultValue>]
    val mutable MarketData: DbSet<MarketData>

    member this.marketdata
        with public get () = this.MarketData
        and public set m = this.MarketData <- m

    override __.OnConfiguring(optionBuilder: DbContextOptionsBuilder) =
        optionBuilder.UseNpgsql(@"Host=localhost:5432;Username=postgres;Password=postgres;Database=postgres")
        |> ignore
