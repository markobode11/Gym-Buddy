﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class BaseUnitOfWork<TDbContext> : BaseUnitOfWork
        where TDbContext : DbContext
    {
        protected readonly TDbContext UowDbContext;

        public BaseUnitOfWork(TDbContext uowDbContext)
        {
            UowDbContext = uowDbContext;
        }

        public override Task<int> SaveChangesAsync()
        {
            return UowDbContext.SaveChangesAsync();
        }
    }
}