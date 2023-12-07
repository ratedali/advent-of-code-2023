module day_03.parts.part2

open day_03.parse
open day_03.utils

type Part =
    | PartNumber of value: int
    | DigitOf of x: int * y: int
    | Star
    | Ignored

type PartGraph = Part[][]


let isSymbol =
    function
    | Symbol _ -> true
    | _ -> false

let toParts (input: Graph) : PartGraph =
    seq {
        let N = input.Length

        for x in 0 .. N - 1 do
            let M = input[x].Length

            yield
                seq {
                    for y in 0 .. M - 1 do
                        match input[x][y] with
                        | Number(value, _, digits) ->
                            if neighbours N M input (x, y) digits |> Seq.exists isSymbol then
                                yield PartNumber value
                            else
                                yield Ignored
                        | NumberDigit y' -> yield DigitOf(x, y')
                        | Symbol('*', _) -> yield Star
                        | _ -> yield Ignored 
                }
                |> Seq.toArray
    }
    |> Seq.toArray

let findGears (graph: PartGraph) =
    seq {
        let N = graph.Length

        for x in 0 .. N - 1 do
            let M = graph[x].Length

            for y in 0 .. M - 1 do
                match graph[x][y] with
                | Star ->
                    let neighbours = neighbours N M graph (x, y) 1

                    let parts =
                        neighbours
                        |> Seq.collect (function
                            | PartNumber value -> [ value ]
                            | DigitOf(x, y) ->
                                match graph[x][y] with
                                | PartNumber value -> [ value ]
                                | _ -> []
                            | _ -> [])
                        |> Seq.toList
                        |> List.distinct

                    match parts with
                    | [ a; b ] -> yield (a, b)
                    | _ -> ()
                | _ -> ()
    }

let solve (data: Graph) =
    data |> toParts |> findGears |> Seq.sumBy (fun (a, b) -> a * b)
