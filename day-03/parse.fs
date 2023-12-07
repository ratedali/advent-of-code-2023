module day_03.parse

type ParseError = | InvalidData

let private (|Digit|Character|) =
    function
    | c when '0' <= c && c <= '9' -> Digit(int (c - '0'))
    | c -> Character c

type Element =
    | Number of value: int * loc: int * digits: int
    | NumberDigit of loc: int
    | Symbol of value: char * loc: int
    | Dot of loc: int

type Graph = Element[][]

let parseLine (line: string) =
    let iter i c acc =
        match c, acc with
        | Digit n, Number(m, l, d) :: acc when i = l - 1 -> Number(n * (pown 10 d) + m, i, d + 1) :: acc
        | Digit n, acc -> Number(n, i, 1) :: acc
        | Character '.', acc -> Dot i :: acc
        | Character c, acc -> Symbol(c, i) :: acc

    let n = String.length line
    let indices = seq { 0 .. (n - 1) }
    let elems = Seq.foldBack2 iter indices line []

    seq {
        for elem in elems do
            match elem with
            | Number(_, loc, digits) as n ->
                yield n
                yield! Seq.replicate (digits - 1) (NumberDigit loc) 
            | _ -> yield elem
    }


let parse (lines: string seq) : Result<Graph, ParseError> =
    lines |> Seq.map (parseLine >> Seq.toArray) |> Seq.toArray |> Result.Ok
