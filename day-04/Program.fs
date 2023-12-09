module day_04.Program

open System.IO

open day_04.parse
open day_04.parts

let parse path1 path2 =
    let data1 = File.ReadAllLines path1 |> parse

    let data2 =
        if path1 = path2 then
            data1
        else
            File.ReadAllLines path2 |> parse

    match data1, data2 with
    | Ok data1, Ok data2 -> Ok(data1, data2)
    | Error e, _
    | _, Error e -> Error e

let solve (data1, data2) =
    let part1 = part1.solve data1
    let part2 = part2.solve data2

    printfn $"Part 1: %d{part1}"
    printfn $"Part 2: %d{part2}"

[<EntryPoint>]
let main args =
    let path1, path2 =
        match args with
        | [| path1; path2; _ |] -> path1, path2
        | [| path |] -> path, path
        | _ -> "input/part1.txt", "input/part2.txt"

    match parse path1 path2 with
    | Ok data ->
        solve data
        0
    | Error e ->
        printfn $"Error: %A{e}"
        1
