namespace Core

open Shared

type IUnityOfWork =
    abstract member MarketData: IRepository<MarketData> with get
    abstract member Save: unit
