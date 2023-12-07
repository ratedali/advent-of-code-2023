module day_02.parse

open System
open utils

type CubeTotal =
    { Red: int
      Green: int
      Blue: int }
    
    static member Zero = { Red = 0; Green = 0; Blue = 0 }

    static member (+)(lhs: CubeTotal, rhs: CubeTotal) =
        match lhs, rhs with
        | { Red = r1; Green = g1; Blue = b1 }, { Red = r2; Green = g2; Blue = b2 } ->
            { Red = r1 + r2
              Green = g1 + g2
              Blue = b1 + b2 }
            

let (|Red|Green|Blue|Invalid|) (input: string) =
    match input.Split " " with
    | [| count; "red" |] ->
        match parseInt count with
        | Some count -> Red count
        | None -> Invalid
    | [| count; "green" |] ->
        match parseInt count with
        | Some count -> Green count
        | None -> Invalid
    | [| count; "blue" |] ->
        match parseInt count with
        | Some count -> Blue count
        | None -> Invalid
    | _ -> Invalid

type Game = { id: int; sets: CubeTotal list }

type ParseError =
    | InvalidGameFormat
    | InvalidIdFormat
    | InvalidSetFormat
    | InvalidCubeCount


let parseCubeTotal =
    function
    | Red count -> Ok { Red = count; Green = 0; Blue = 0 }
    | Green count -> Ok { Red = 0; Green = count; Blue = 0 }
    | Blue count -> Ok { Red = 0; Green = 0; Blue = count }
    | Invalid -> Error InvalidCubeCount


let parseSet (set: string) : Result<CubeTotal, ParseError> =
    set.Split(',', StringSplitOptions.TrimEntries)
    |> Seq.map parseCubeTotal
    |> flattenResults
    |> Result.map Seq.sum

let parseSets (sets: string) : Result<CubeTotal list, ParseError> =
    sets.Split(';', StringSplitOptions.TrimEntries)
    |> Seq.map parseSet
    |> flattenResults
    |> Result.map Seq.toList


let parseId (id: string) : Result<int, ParseError> =
    if id.StartsWith "Game " then
        match Int32.TryParse id[5..] with
        | true, id -> Ok id
        | _ -> Error InvalidIdFormat
    else
        Error InvalidIdFormat


let parseGame (line: string) =
    match line.Split(':', StringSplitOptions.TrimEntries) with
    | [| id; sets |] ->
        match parseId id, parseSets sets with
        | Ok id, Ok sets -> Ok { id = id; sets = sets }
        | Error e, _ -> Error e
        | _, Error e -> Error e
    | _ -> Error InvalidGameFormat

let parseGames (input: string seq) =
    input |> Seq.map parseGame |> flattenResults |> Result.map Seq.toList
