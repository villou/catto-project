﻿using Microsoft.EntityFrameworkCore;

namespace catto.Models;

public class CattoContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Token> Tokens { get; set; }

    public string DbPath { get; }
    
    public CattoContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "catto.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
