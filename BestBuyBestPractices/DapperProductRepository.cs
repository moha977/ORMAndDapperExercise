using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BestBuyBestPractices
{
    internal class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;

        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }
        public void CreateProduct(string newName, double newPrice, int newCategoryID)
        {
            _connection.Execute("INSERT INTO Products (Name , price , CategoryID) VALUES (@Name , @price , @CategoryID);",
            new { Name = newName , Price = newPrice , CategoryID = newCategoryID });

        }
        public void UpdateProduct(string updatedName, int ID)
        {
            _connection.Execute("Update Products Set Name = @Name Where productID = @ID;", new { name = updatedName, ID = ID });
        }
       public void DeleteProduct(int iD)
        {
            _connection.Execute("Delete From products Where ProductID = @iD;", new {iD = iD});
            _connection.Execute("Delete From reviews Where ProductID = @iD;", new {iD = iD});
            _connection.Execute("Delete From sales Where ProductID = @iD;", new {iD = iD});
        }

    }

      
    
}
