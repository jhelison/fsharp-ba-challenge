namespace Core

open Models

type IUnityOfWork =
    abstract member MarketData: IRepository<MarketData> with get
    abstract member Save: unit
