module AStar

type Config<'a> = 
    {
        /// <summary>
        /// A method that, given a source, will return its neighbours
        /// </summary>
        neighbours: 'a -> seq<'a>
        /// <summary>
        /// Given two nodes that are next to each other, return the g cost between them
        /// The g cost is the cost of moving from one to the other directly
        /// </summary>
        gCost: 'a -> 'a -> float
        /// <summary>
        /// Given two nodes, return the f cost between them. This is a heuristic, and is used from a given node to the goal
        /// Line of site distance is an example of how this might be expressed
        /// </summary>
        fCost: 'a -> 'a -> float
    }

let private reconstructPath cameFrom final =
    [final]

let search<'a> start goal config =
    let rec crawler openSet closedSet gScores fScores cameFrom =
        match List.sortBy (fun n -> Map.find n fScores) openSet with
        | current::_ when current = goal -> Some <| reconstructPath cameFrom current 
        | current::rest ->
            let gScore = Map.find current gScores
            let neighbours = 
                config.neighbours current 
                |> Seq.filter (fun n -> closedSet |> Set.contains n |> not)
                |> Seq.map (fun n -> n, gScore + config.gCost current n)
                |> Seq.filter (fun (n, gs) -> Map.containsKey n gScores |> not || gScores.[n] < gs)
            let newOpen = rest @ (Seq.filter (fun (n, _) -> List.contains n |> not) neighbours |> Seq.toList)
            crawler rest (Set.add current closedSet) gScores fScores cameFrom
        | _ -> None
    let gScores = Map.ofList [start, 0.]
    let fScores = Map.ofList [start, config.fCost start goal]
    crawler [start] Set.empty gScores fScores Map.empty