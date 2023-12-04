module day_02.part1

open parse

let countTotal (set: GameSet) =
    set
    |> Seq.fold
        (fun acc count ->
            match count with
            | Red n -> { acc with Red = acc.Red + n }
            | Green n -> { acc with Green = acc.Green + n }
            | Blue n -> { acc with Blue = acc.Blue + n })
        { Red = 0; Green = 0; Blue = 0 }

let impossibleCubeCount bagTotal set =
    match countTotal set with
    | { Red = r; Green = g; Blue = b } -> (r > bagTotal.Red) || (g > bagTotal.Green) || (b > bagTotal.Blue)

let impossibleSet bagTotal (game: Game) =
    game.sets |> Seq.tryFind (impossibleCubeCount bagTotal)

let impossible bagTotal games =
    games
    |> List.map (fun game ->
        let set = game |> impossibleSet bagTotal
        (game.id, set))

let solve bagTotal games =
    games
    |> impossible bagTotal
    |> List.sumBy (function
        | id, None -> id
        | _, Some _ -> 0)
