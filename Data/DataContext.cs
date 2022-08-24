using Microsoft.EntityFrameworkCore;

namespace ApiDotnetEntityFramework.Data;

public class DataContext : DbContext{
    public DataContext(DbContextOptions<DataContext> options) : base(options){
        
    }
    public DbSet<Account> accounts { get; set; }
}