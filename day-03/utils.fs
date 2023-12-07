module day_03.utils


let neighbourCoords N M (x, y) len =
    let Y = y + len - 1
    seq {
        // same row
        if y > 0 then yield (x, y - 1)
        if Y < M - 1 then yield (x, Y + 1)
        // row above
        if x > 0 then
            // same columns
            yield! [y .. Y] |> Seq.map (fun y -> (x - 1, y))
            // top-left diagonal
            if y > 0 then yield (x - 1, y - 1)
            // top-right diagonal
            if Y < M - 1 then yield (x - 1, Y + 1)
        // row below
        if x < N - 1 then
            // same columns
            yield! [y .. Y] |> Seq.map (fun y -> (x + 1, y))
            // bottom-left diagonal
            if y > 0 then yield (x + 1, y - 1)
            // bottom-right diagonal
            if Y < M - 1 then yield (x + 1, Y + 1)
    }


let neighbours N M (matrix: 'a[][]) (x, y) len =
    neighbourCoords N M (x, y) len |> Seq.map (fun (x, y) -> matrix[x][y])  