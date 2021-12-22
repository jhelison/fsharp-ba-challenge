namespace Core

open System.Collections.Generic

type IRepository<'T> =
    abstract member All: IEnumerable<'T>
    abstract member Get: int -> 'T
    abstract member Add: 'T -> 'T
    abstract member Delete: int -> unit
    abstract member Update: 'T -> 'T
