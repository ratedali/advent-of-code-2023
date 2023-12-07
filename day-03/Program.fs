module day_03

open day_03.parse
open day_03.parts

let load path1 path2 =
    let data1 = path1 |> System.IO.File.ReadAllLines |> parse
    let data2 = path2 |> System.IO.File.ReadAllLines |> parse

    match data1, data2 with
    | Ok data1, Ok data2 -> Ok(data1, data2)
    | Error e, _
    | _, Error e -> Error e


let solve (data1, data2) =
    let part1 = part1.solve data1
    let part2 = part2.solve data2
    part1, part2

[<EntryPoint>]
let main args =
    let path1 = args |> Array.tryHead |> Option.defaultValue "input/part1.txt"
    let path2 = args |> Array.tryItem 1 |> Option.defaultValue path1
    let result = load path1 path2 |> Result.map solve

    match result with
    | Ok(part1, part2) ->
        printfn $"Part 1: %A{part1}"
        printfn $"Part 2: %A{part2}"
        0
    | Error e ->
        eprintfn $"Error: %s{e}"
        1
