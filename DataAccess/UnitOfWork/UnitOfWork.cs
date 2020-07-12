using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IArticleRepository _articleRepository;

        public UnitOfWork(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public IArticleRepository ArticleRepository
        {
            get
            {
                return _articleRepository;
            }
        }
    }
}
