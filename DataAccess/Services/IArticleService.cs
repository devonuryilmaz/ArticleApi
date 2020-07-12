using DataAccess.Entities;
using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IArticleService
    {
        public Task<IEnumerable<Article>> GetAll();

        Task<bool> Delete(int id);
        Task<Article> Get(int id);
        Task<bool> Update(Article t);
        Task<bool> Insert(Article t);

    }
}
