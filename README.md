# astar-search

![.NET](https://github.com/ChrisPritchard/astar-search/actions/workflows/dotnet.yml/badge.svg) [![NuGet](https://img.shields.io/nuget/v/astar-search.svg)](https://www.nuget.org/packages/astar-search/)

A* Search algorithm in F#.

An implementation of the classic algorithm, as described here: <https://en.wikipedia.org/wiki/A*_search_algorithm.>

However, in contrast to the pseudo-code shown in that article, this version is implemented in a purely functional and immutable way, as is more idiomatic for F#.

The core code is in the [src/AStar.fs](./src/AStar.fs) module. The algorithm requires callers submit a start point, goal point, and three functions: a gscore calculator, fscore calculator and a method that returns the neighbours of a given point. In this way, the algorithm is generalisable to any type of point or graph structure - the only constraint on the generic point type is that it be comparable.

[sample/Program.fs](./sample/Program.fs) contains a console application demonstrating use of the algorithm, finding a path through a two dimensional grid.

Enjoy!
