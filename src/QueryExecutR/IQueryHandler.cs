using System.Threading;
using System.Threading.Tasks;

namespace QueryExecutR
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Execute(TQuery query, CancellationToken cancellationToken = default);
    }
}