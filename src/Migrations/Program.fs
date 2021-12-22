module Program

open System.Reflection
open SimpleMigrations
open Microsoft.Data.Sqlite
open SimpleMigrations.DatabaseProvider
open SimpleMigrations.Console
open Npgsql


[<EntryPoint>]
let main argv =
    let assembly = Assembly.GetExecutingAssembly()
    use db = new NpgsqlConnection "Host=localhost:5432;Username=postgres;Password=postgres;Database=postgres"
    let provider = PostgresqlDatabaseProvider(db)
    let migrator = SimpleMigrator(assembly, provider)
    let consoleRunner = ConsoleRunner(migrator)
    consoleRunner.Run(argv) |> ignore
    0
