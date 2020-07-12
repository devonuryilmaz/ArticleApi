using DataAccess.Entities;
using DataAccess.Infrastructure;

namespace DataAccess.Repositories
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        IConnectionFactory _connectionFactory;
        public ArticleRepository(IConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
            _connectionFactory = connectionFactory;
            this._tableName = "Article";
        }

        //public ArticleRepository()
        //{

        //}

    }
}
