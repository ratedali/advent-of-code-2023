module day_02.parts.part2

open day_02
open day_02.parse

let private elementwiseMax lhs rhs =
    { Red = max lhs.Red rhs.Red
      Green = max lhs.Green rhs.Green
      Blue = max lhs.Blue rhs.Blue }

let minRequiredCount set = set |> Seq.fold elementwiseMax CubeTotal.Zero

let power =
    function
    | { Red = red
        Green = green
        Blue = blue } -> red * green * blue
    

let solve bagTotal games =
    games
    |> Seq.map (fun { id = id; sets = set } -> id, minRequiredCount set)
    |> Seq.sumBy (snd >> power)
