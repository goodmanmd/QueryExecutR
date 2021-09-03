using System.Threading;
using System.Threading.Tasks;

namespace QueryExecutR.Wrappers
{
    public abstract class QueryHandlerBase
    {
        public abstract Task<object?> Execute(object request, CancellationToken cancellationToken, QueryHandlerFactory serviceFactory);
    }

    public abstract class QueryHandlerWrapper<TResult> : QueryHandlerBase
    {
        public abstract Task<TResult> Execute(IQuery<TResult> query, CancellationToken cancellationToken,
            QueryHandlerFactory factory);
    }

    public class QueryHandlerWrapperImpl<TQuery, TResult> : QueryHandlerWrapper<TResult>
        where TQuery : IQuery<TResult>
    {
        public override async Task<object?> Execute(object request, CancellationToken cancellationToken, QueryHandlerFactory serviceFactory) =>
            await Execute((IQuery<TResult>)request, cancellationToken, serviceFactory).ConfigureAwait(false);

        public override Task<TResult> Execute(IQuery<TResult> query, CancellationToken cancellationToken, QueryHandlerFactory factory)
        {
            var handler = factory.GetInstance<IQueryHandler<TQuery, TResult>>();
            return handler.Execute((TQuery)query, cancellationToken);
        }
    }
}
