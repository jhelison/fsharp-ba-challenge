namespace Core

open System.Collections.Generic
open Models
open Shared

type IMarketDataRepository =
    inherit IRepository<MarketData>

    abstract member Filter: MarketDataFilters -> IEnumerable<MarketData>
