using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using DataAccess.Repositories;
using DataAccess.Entities;
using System.Threading.Tasks;


namespace DataAccess.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await _unitOfWork.ArticleRepository.DeleteRowAsync(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Article> Get(int id)
        {
            return await _unitOfWork.ArticleRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _unitOfWork.ArticleRepository.GetAllAsync();
        }

        public async Task<bool> Insert(Article t)
        {
            try
            {
                await _unitOfWork.ArticleRepository.InsertAsync(t);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Update(Article t)
        {
            try
            {
                await _unitOfWork.ArticleRepository.UpdateAsync(t);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
