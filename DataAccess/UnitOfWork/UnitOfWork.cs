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

        public void Complete()
        {
            throw new NotImplementedException();
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
