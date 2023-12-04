module day_02.Program

open System.IO
open parse

let BagTotal = { Red = 12; Green = 13; Blue = 14 }

[<EntryPoint>]
let main (args) =
    args
    |> Array.head
    |> File.ReadAllLines
    |> parseGames
    |> Result.map (part1.solve BagTotal >> fun n -> printfn $"Part 1 = %d{n}")
    |> function
        | Ok _ -> 0
        | Error e ->
            printfn $"Error: {e}"
            1
