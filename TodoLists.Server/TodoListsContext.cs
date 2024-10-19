using Microsoft.EntityFrameworkCore;

namespace TodoLists.Server;

public partial class TodoListsContext : DbContext
{
    public TodoListsContext() { }

    public TodoListsContext(DbContextOptions<TodoListsContext> options)
        : base(options) { }

    public virtual DbSet<Todo> Todos { get; set; }

    public virtual DbSet<TodoList> TodoLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.ToTable("Todo");

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

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
