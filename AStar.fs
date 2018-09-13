module AStar

type Config<'a> = 
    {
        
        neighbours: 'a -> seq<'a>
        gCost: 'a -> 'a -> float
        hCost: 'a -> 'a -> float
    }

let search<'a> start goal config =
    seq {
        yield start
    }