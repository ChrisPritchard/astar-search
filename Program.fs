
[<EntryPoint>]
let main _ =
    let testArray = 
        [
            [' ';' ';' ';' ';' ';' ';' ';' ';' ';' ']
            [' ';' ';' ';' ';' ';' ';' ';' ';' ';' ']
            [' ';' ';' ';' ';' ';' ';' ';' ';' ';' ']
            [' ';' ';' ';' ';' ';' ';' ';'X';'X';' ']
            [' ';' ';' ';' ';' ';' ';'X';'X';' ';' ']
            [' ';' ';' ';' ';' ';' ';'X';' ';' ';' ']
            [' ';' ';' ';' ';'X';'X';'X';' ';'X';'X']
            [' ';' ';' ';' ';'X';' ';' ';' ';'X';' ']
            [' ';' ';' ';' ';'X';' ';'X';'X';'X';' ']
            [' ';' ';' ';' ';'X';' ';'X';' ';' ';' ']
            [' ';' ';' ';' ';'X';' ';'X';' ';' ';' ']
            [' ';' ';' ';' ';'X';' ';' ';' ';'X';' ']
            [' ';' ';' ';' ';' ';' ';'X';'X';'X';' ']
            [' ';' ';' ';' ';' ';' ';'X';' ';' ';' ']
            [' ';' ';' ';' ';' ';' ';'X';' ';' ';' ']
        ]
    let width, height = List.length testArray.[0], List.length testArray

    let start = 0, 0
    let goal = width - 1, height - 1

    let blocks = 
        testArray 
        |> List.mapi (fun y row -> 
            row 
                |> List.mapi (fun x cell -> (x, y), cell = 'X')
                |> List.filter (fun (_, blocked) -> blocked)
                |> List.map (fun (pos, _) -> pos))
        |> List.concat |> Set.ofList
    let neighbours (x, y) =
        let found = 
            [-1..1] |> List.collect (fun nx ->
            [-1..1] |> List.filter (fun ny -> ny <> nx || ny <> 0) |> List.map (fun ny -> x + nx, y + ny))
        found |> Seq.filter (fun (nx, ny) -> 
            nx > 0 && ny > 0 &&
            nx < width && ny < height &&
            not <| Set.contains (nx, ny) blocks)

    let gScore _ _ = 1.
    let fScore (x, y) (gx, gy) = 
        sqrt ((float gx - float x)**2. + (float gy - float y)**2.)

    match AStar.search start goal { neighbours = neighbours; gCost = gScore; fCost = fScore } with
    | Some path -> 
        printf "Success! Solution:"
        testArray |> List.mapi (fun y row -> 
            row 
            |> List.mapi (fun x cell -> if Seq.contains (x, y) path then "#" else cell.ToString())
            |> String.concat ""
        ) |> List.iter (printfn "%s")
    | None -> printf "No Path Found"

    0