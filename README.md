# QueryExecutR
Stripped down version of *MediatR* meant for creating / running queries over a single dispatch interface.

## Use Case
The primary use case for *QueryExecutR* is... executing queries in a CQS style application. One approach to using CQS is to create a class for each different query type in your application, and then use these query classes as dependencies, possibly injected by a DI container.  Like *MediatR*, *QueryExecutR* takes one step beyond that pattern and introduces a single `IQueryExecutor` interface so that any query can be executed from that single dependency using polymoprhic dispatch.

## Why not just use *MediatR*?
If you're taking the route of creating classes for each of the queries in your application, chances are there's some re-usability between these queries, and perhaps even areas where you may consume results from multiple queries from the same class. However, *MediatR* includes the concept of a request pipeline and pipeline behaviors. The library is not designed for chaining multiple commands together in the same request, which can result undesired or undefined behaviors (see what I did there).

*QueryExecutR* has no pipeline and no behaviors to speak of, so it doesn't have the concern of chaining queries or otherwise running more than one query per request.
