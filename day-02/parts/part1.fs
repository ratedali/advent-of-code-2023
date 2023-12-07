module day_02.parts.part1

open day_02.parse

let isSetImpossible bagTotal set =
    (set.Red > bagTotal.Red)
    || (set.Green > bagTotal.Green)
    || (set.Blue > bagTotal.Blue)

let getImpossibleSets bagTotal game =
    match game with
    | { sets = sets } -> sets |> Seq.filter (isSetImpossible bagTotal)

let findImpossibleSets bagTotal games =
    let ids = games |> Seq.map (fun { id = id } -> id)
    games |> Seq.map (getImpossibleSets bagTotal) |> Seq.zip ids

let solve bagTotal =
    findImpossibleSets bagTotal >> Seq.filter (snd >> Seq.isEmpty) >> Seq.sumBy fst
