let add1 x = x+1

let random = fun n-> 10

type Deal =
    {Deck : int->int}

let useFn f = f() + 1

let hello = printfn "Hello, my name is %s"


["Sambaran";"Debangana";"Chorki"]
|> List.iter hello

Some 1 |> Option.map add1

type OrderLine = 
    {
        Qty:int
        Total:float
    }

let orderLines = [
    {Qty=2; Total=3.45}
    {Qty=1; Total=13.05}
    {Qty=5; Total=12.30}
]

let addPair line1 line2=
    let newQty = line1.Qty+line2.Qty
    let newTotal = line1.Total+line2.Total
    {Qty=newQty;Total=newTotal}

orderLines |> List.reduce addPair


[1.1;6.7] |> List.map (fun n -> n*n) |> List.reduce (+)