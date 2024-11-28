
using Microsoft.EntityFrameworkCore;
using TodoLists.Domain;

namespace TodoLists.API.Data;

internal class TodoListDbContext : DbContext
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

    // get todolist summaries
    // get todolist detail (id)
    // get todo (id)

    // create todolist
    // create todo

    // delete todolist
    // delete todo

    // rename todolist
    // update to do - change desc, due date, todolist; toggle importance, toggle complete
}
