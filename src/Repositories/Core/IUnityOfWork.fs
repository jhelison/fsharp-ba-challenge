namespace Core

open Models

type IUnityOfWork =
    abstract member MarketData: IMarketDataRepository with get
    abstract member Save: unit
