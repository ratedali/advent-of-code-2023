module day_02.Program

open System.IO
open parse

let loadData path = File.ReadAllLines path |> parseGames

let BagTotal = { Red = 12; Green = 13; Blue = 14 }

let solve games =
    let part1 = parts.part1.solve BagTotal games
    let part2 = parts.part2.solve BagTotal games
    part1, part2

[<EntryPoint>]
let main (args) =
    let path = args |> Array.tryHead |> Option.defaultValue "input/part1.txt"
    let res = path |> loadData |> Result.map solve

    match res with
    | Ok(p1, p2) ->
        printfn $"Part 1 answer: %d{p1}"
        printfn $"Part 2 answer: %d{p2}"
        0
    | Error e ->
        eprintfn $"Error: %A{e}"
        1
