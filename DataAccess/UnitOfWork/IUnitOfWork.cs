using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        IArticleRepository ArticleRepository { get; }
        void Complete();
    }
}
