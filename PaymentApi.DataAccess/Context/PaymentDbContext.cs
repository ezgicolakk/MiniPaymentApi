using Microsoft.EntityFrameworkCore;
using PaymentApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.DataAccess.Context
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=LAPTOP-GLLE6F7G; Database=PaymentDb; User Id=sa; Password=1234; TrustServerCertificate=true;");
        }
    }
}
