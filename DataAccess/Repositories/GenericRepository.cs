using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        IConnectionFactory _connectionFactory;
        protected string _tableName;

        protected GenericRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore" 
                    select prop.Name).ToList();
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName}");

            insertQuery.AppendLine("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop =>
            {
                if (!prop.Equals("Id"))
                    insertQuery.Append($"[{prop}],");
            });

            insertQuery.Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop =>
            {
                if (!prop.Equals("Id"))
                    insertQuery.Append($"@{prop},");
            });

            insertQuery.Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");

            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(prop =>
            {
                if (!prop.Equals("Id"))
                    updateQuery.Append($"{prop} = @{prop},");
            });

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append("    WHERE Id = @Id");


            return updateQuery.ToString();
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        public async Task DeleteRowAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] couldnt found");

                return result;
            }
        }

        public async Task InsertAsync(T t)
        {
            var insertQuery = GenerateInsertQuery();

            using (var connection = _connectionFactory.GetConnection)
            {
                await connection.ExecuteAsync(insertQuery, t);
            }
        }

        public async Task UpdateAsync(T t)
        {
            var updateQuery = GenerateUpdateQuery();

            using (var connection = _connectionFactory.GetConnection)
            {
                await connection.ExecuteAsync(updateQuery, t);
            }
        }
    }
}
