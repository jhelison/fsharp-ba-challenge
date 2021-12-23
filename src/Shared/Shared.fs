namespace Shared

open System

[<CLIMutable>]
type MarketDataFilters =
    { startDate: DateTime option
      endDate: DateTime option
      minPrice: float option
      maxPrice: float option
      minQuantity: int option
      maxQuantity: int option }
