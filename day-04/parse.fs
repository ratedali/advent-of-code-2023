module day_04.parse

open System

type ParseError =
    | InvalidInput
    | InvalidCard of string
    | InvalidCardId of string

type Card =
    { id: int
      winning: int array
      have: int array }

type Input = Card array

let parseNumberList (numbers: string) =
    numbers.Split(' ', StringSplitOptions.TrimEntries ||| StringSplitOptions.RemoveEmptyEntries)
    |> Array.map Int32.Parse

let parseCardNumbers (numbers: string) =
    match numbers.Split("|", StringSplitOptions.TrimEntries) with
    | [| winning; have |] -> Ok(parseNumberList winning, parseNumberList have)
    | _ -> Error(InvalidCard numbers)

let parseCardId (id: string) =
    match Int32.TryParse id[4..] with
    | true, id -> Ok id
    | _ -> Error(InvalidCardId id)

let parseLine (line: string) =
    match line.Split ":" with
    | [| id; numbers |] ->
        match parseCardId id, parseCardNumbers numbers with
        | Ok id, Ok(winning, have) ->
            Ok
                { id = id
                  winning = winning
                  have = have }
        | Error e, _
        | _, Error e -> Error e
    | _ -> Error(InvalidCard line)

let parse (data: string seq) : Result<Input, ParseError> =
    data
    |> Seq.map parseLine
    |> Seq.toArray
    |> Seq.fold
        (fun acc x ->
            match acc, x with
            | Ok acc, Ok x -> Ok(x :: acc)
            | Error e, _
            | _, Error e -> Error e)
        (Ok [])
    |> Result.map (Seq.toArray >> Array.rev)
