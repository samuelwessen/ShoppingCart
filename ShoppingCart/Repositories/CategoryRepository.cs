using ShoppingCart.Data;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly SqlDbContext _sqlDbContext;

        public CategoryRepository(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public IEnumerable<Category> Categories => _sqlDbContext.Categories;
    }
}
