module day_04.parts.part1

open day_04
open parse

let commonNumbers (a: int seq) (b: int seq) = Set.intersect (set a) (set b)

let winningSet =
    function
    | { winning = winning; have = have } -> commonNumbers winning have

let value =
    function
    | 0 -> 0
    | n -> pown 2 (n - 1)

let solve (data: Input) : int =
    data |> Seq.sumBy (winningSet >> Set.count >> value)
