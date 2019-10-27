open System
open System.IO

let toupledAdd (a, b) = a + b

let curriedAdd a b c = a + b + c

let curriedAddOne = curriedAdd 1

let buildDate year month day = DateTime(year, month, day)

let buildDateThisYear = buildDate System.DateTime.Now.Year

let buildDateThisMonth = buildDateThisYear System.DateTime.Now.Month

let add a = fun b c -> a + b + c

let replace (oldValue: string) newValue (str: string) = str.Replace(oldValue, newValue)


let convertNumberToRoman number =
    String.replicate number "I"
    |> replace "IIIII" "V"
    |> replace "VV" "X"
    |> replace "XXXXX" "L"
    |> replace "LL" "C"
    |> replace "CCCCC" "D"
    |> replace "DD" "M"
    |> replace "VIIII" "IX"
    |> replace "IIII" "IV"
    |> replace "LXXXX" "XC"

type CarbonatedResult =
    | Uncarbonated of int
    | Carbonated of string

let isDivisibleBy num divisor = num % divisor = 0

let carbonate divisor label n =
    if (isDivisibleBy n divisor) then Carbonated label
    else Uncarbonated n

let ifUncarbonatedDo f (input: CarbonatedResult) =
    match input with
    | Uncarbonated n -> f n
    | Carbonated n -> Carbonated n

let result (input: CarbonatedResult) =
    match input with
    | Uncarbonated n -> string n
    | Carbonated n -> n

let fizzBuzz num =
    num
    |> carbonate 15 "FizzBuzz"
    |> ifUncarbonatedDo (carbonate 3 "Fizz")
    |> ifUncarbonatedDo (carbonate 5 "Buzz")
    |> result

[ 1 .. 100 ]
|> List.map fizzBuzz
|> List.iter (printfn "%s")

let sumOfSquares num = [ 1 .. num ] |> List.sumBy (fun x -> x * x)

let rec quicksort list =
    match list with
    | [] -> [] // return an empty list
    | firstElem :: otherElements ->
        let smallerElements = // extract the smaller ones
            otherElements
            |> List.filter (fun e -> e < firstElem)
            |> quicksort // and sort them

        let largerElements = // extract the large ones
            otherElements
            |> List.filter (fun e -> e >= firstElem)
            |> quicksort // and sort them
        // Combine the 3 parts into a new list and return it
        smallerElements @ [ firstElem ] @ largerElements

let rec quicksort2 =
    function
    | [] -> []
    | first :: rest ->
        let smaller, larger = List.partition ((>=) first) rest
        List.concat
            [ quicksort2 smaller
              [ first ]
              quicksort2 larger ]

type AppleType = string

type BananaType = string

type FruitSalad =
    { Apple: AppleType
      Banana: BananaType }

let (|Int|_|) (str: string) =
    match System.Int32.TryParse(str) with
    | (true, int) -> Some(int)
    | _ -> None

let (|Bool|_|) (str: string) =
    match System.Boolean.TryParse(str) with
    | (true, bool) -> Some(bool)
    | _ -> None

let testParse (str: string) =
    match str with
    | Int i -> printfn "The value is an int '%i'" i
    | Bool b -> printfn "The value is a bool '%b'" b
    | _ -> printfn "The value '%s' is something else" str

type Person =
    { FirstName: string
      LastName: string }

let sambaran =
    { FirstName = "Sambaran"
      LastName = "Hazra" }

let debangana = { sambaran with FirstName = "Debangana" }

type State =
    | New
    | Draft
    | Published
    | Inactive
    | Discontinued

let handleState state =
    match state with
    | New -> Some New
    | Draft -> Some Draft
    | _ -> None

let rec movingAverage (list: int list) =
    match list with
    | [] -> []
    | x :: y :: rest ->
        let avg = (float x + float y) / 2.0
        avg :: movingAverage (y :: rest)
    | [ _ ] -> []

type Result<'a, 'b> =
    | Success of 'a
    | Failure of 'b

type FileErrorReason =
    | FileNotFound of string
    | UnauthorizedAccess of string * System.Exception

let performActionOnFile action filePath =
    try
        use sr = new System.IO.StreamReader(filePath: string)
        let result = action sr
        sr.Close()
        Success(result)
    with
    | :? System.IO.FileNotFoundException as ex -> Failure(FileNotFound filePath)
    | :? System.Security.SecurityException as ex -> Failure(UnauthorizedAccess(filePath, ex))