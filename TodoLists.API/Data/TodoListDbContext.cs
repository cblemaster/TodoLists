
using Microsoft.EntityFrameworkCore;

using TodoLists.Domain;

namespace TodoLists.API.Data;

internal partial class TodoListDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; } = default!;
    public DbSet<TodoList> TodoLists { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.ToTable("Todo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.HasOne(d => d.TodoList).WithMany(p => p.Todos)
                .HasForeignKey(d => d.TodoListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Todo_TodoList");
        });
        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.ToTable("TodoList");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });
    }
}
