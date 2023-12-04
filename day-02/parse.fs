module day_02.parse

open System
open utils

type CubeTotal = { Red: int; Green: int; Blue: int }
type CubeCount =
    | Red of int
    | Green of int
    | Blue of int
type GameSet = CubeCount list
type Game = { id: int; sets: GameSet list }

type ParseError =
    | InvalidGameFormat
    | InvalidIdFormat
    | InvalidSetFormat
    | InvalidCubeCount


let parseCubeCount (count: string) =
    match count.Split " " with
    | [| count; "red" |] ->
        match parseInt count with
        | Some count -> Ok(Red count)
        | None -> Error InvalidCubeCount
    | [| count; "green" |] ->
        match parseInt count with
        | Some count -> Ok(Green count)
        | None -> Error InvalidCubeCount
    | [| count; "blue" |] ->
        match parseInt count with
        | Some count -> Ok(Blue count)
        | None -> Error InvalidCubeCount
    | _ -> Error InvalidCubeCount


let parseSet (set: string): Result<GameSet, ParseError> =
    set.Split(',', StringSplitOptions.TrimEntries)
    |> Seq.map parseCubeCount
    |> flattenResults
    |> Result.map Seq.toList

let parseSets (sets: string): Result<GameSet list, ParseError> =
    sets.Split(';', StringSplitOptions.TrimEntries)
    |> Seq.map parseSet
    |> flattenResults
    |> Result.map Seq.toList


let parseId (id: string): Result<int, ParseError> =
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
    input
    |> Seq.map parseGame
    |> flattenResults
    |> Result.map Seq.toList
