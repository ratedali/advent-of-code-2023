module day_04.parts.part2

open day_04
open utils
open parse
open day_04.parts

let init (cards: Card seq) =
    cards |> Seq.map (fun card -> card, 1) |> List.ofSeq

let processCards (cards: Card seq) =
    let rec iter (cards: (Card * int) list) (acc: (Card * int) list) =
        match cards with
        | [] -> acc
        | (card, dupes) as current :: cards ->
            let wins = card |> part1.winningSet |> Set.count

            let (toClone, rest) =
                if List.length cards < wins then
                    cards, []
                else
                    List.splitAt wins cards

            let cloned = toClone |> List.map (fun (card, count) -> card, count + dupes)

            iter (cloned @ rest) (current :: acc)

    iter (init cards) []

let solve (data: Input) : int = data |> processCards |> Seq.sumBy snd
