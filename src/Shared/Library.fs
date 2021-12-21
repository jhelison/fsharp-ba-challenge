namespace Shared

open System

[<CLIMutable>]
type MarketData =
    {
        id: int
        date: DateTime
        pair: string
        price: float
        quantity: int
    }
