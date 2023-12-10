module day_xx.Program

open System.IO

open day_xx.parse
open day_xx.parts


let load_data (path1, path2) =
    let data1 = File.ReadAllLines path1 |> parse

    let data2 =
        if path2 = path1 then
            data1
        else
            File.ReadAllLines path2 |> parse

    match data1, data2 with
    | Ok data1, Ok data2 -> Ok(data1, data2)
    | Error e, _
    | _, Error e -> Error e


let solve (data1, data2) =
    let result1 = data1 |> part1.solve
    let result2 = data2 |> part2.solve

    printfn $"Part 1: %A{result1}"
    printfn $"Part 2: %A{result2}"


[<EntryPoint>]
let main argv =
    let path1, path2 =
        match Array.truncate 2 argv with
        | [| path1; path2 |] -> path1, path2
        | [| path1 |] -> path1, path1
        | _ -> "input/part1.txt", "input/part2.txt"

    match load_data (path1, path2) with
    | Ok data ->
        solve data
        0
    | Error e ->
        eprintfn $"Error: %A{e}"
        1
