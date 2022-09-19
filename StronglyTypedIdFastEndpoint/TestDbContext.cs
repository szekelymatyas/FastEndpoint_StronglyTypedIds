using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace StronglyTypedIdFastEndpoint;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Book> Books { get; set; } = default!;
}

public class Factory : IDesignTimeDbContextFactory<BookDbContext>
{
    public BookDbContext CreateDbContext(string[] args)
    {
        var option = new DbContextOptionsBuilder();
        option.UseSqlite("Data Source=test.db");
        return new BookDbContext(option.Options);
    }
}
