module day_03.parts.part1

open day_03.parse
open day_03.utils

let isSymbol =
    function
    | Symbol _ -> true
    | _ -> false

let findPartNumbers (input: Graph) =
    seq {
        let N = input.Length

        for x in 0 .. N - 1 do
            let M = input[x].Length

            for y in 0 .. M - 1 do
                match input[x][y] with
                | Number (value, _, digits) ->
                    if neighbours N M input (x, y) digits |> Seq.exists isSymbol then
                        yield value
                | _ -> ()
    }

let solve (data: Graph) = data |> findPartNumbers |> Seq.sum
