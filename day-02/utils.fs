module day_02.utils

let tee fn sequence = seq { for x in sequence do fn x; yield x }
let effect fn x =
    do fn x
    x

let parseHelper (f: string -> bool * 'a) =
    f >> function
        | true, x -> Some x
        | false, _ -> None
let parseInt = parseHelper System.Int32.TryParse

let flattenResults results =
    results
    |> Seq.fold
        (fun acc res ->
            match acc, res with
            | Ok acc, Ok res ->
                Ok(
                    seq {
                        yield! acc
                        yield res
                    }
                )
            | Error err, _ -> Error err
            | _, Error err -> Error err)
        (Ok Seq.empty)