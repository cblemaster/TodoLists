using Microsoft.EntityFrameworkCore;
using TodoLists.Web.Entities.Lists;
using TodoLists.Web.Entities.Todos;

namespace TodoLists.Web.Data;

public class TodoContext : DbContext
{
    public DbSet<Todo> Todos { get; set; } = default!;
    public DbSet<ListDetail> ListDetails { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(@"Server=.;Database=ListDetails;Trusted_Connection=true;Trust Server Certificate=true")
                      .UseSeeding((context, b) =>
                      {
                          context.Set<ListDetail>().AddRange(GetSeedData());
                          context.SaveChanges();
                      })
                      .UseAsyncSeeding(async (context, b, cancel) =>
                      {
                          context.Set<ListDetail>().AddRange(GetSeedData());
                          await context.SaveChangesAsync(cancel);
                      });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.ToTable("Todo");
            entity.HasKey(e => e.TodoId);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.HasOne(d => d.List).WithMany(p => p.Todos)
                .HasForeignKey(d => d.ListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Todo_ListDetail");
        });

        modelBuilder.Entity<ListDetail>(entity =>
        {
            entity.ToTable("List");
            entity.HasKey(e => e.ListId);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });
    }

    private static IEnumerable<ListDetail> GetSeedData()
    {
        //ListDetail list1 = new ListDetail() { ListDetailId = 1, Name = "Outside" };
        //ListDetail list2 = new ListDetail() { ListDetailId = 2, Name = "Work" };
        //ListDetail list3 = new ListDetail() { ListDetailId = 3, Name = "Vacation planning" };
        //ListDetail list4 = new ListDetail() { ListDetailId = 4, Name = "Kitchen" };
        //ListDetail list5 = new ListDetail() { ListDetailId = 5, Name = "Everything else..." };
        //Todo todo1 = new Todo() { TodoId = 1, Title = "Water lawn", DueDate = null, IsImportant = false, IsComplete = false };
        //Todo todo2 = new Todo() { TodoId = 2, Title = "Paint fence", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(11)), IsImportant = true, IsComplete = false };
        //Todo todo3 = new Todo() { TodoId = 3, Title = "Clean grill", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(6)), IsImportant = false, IsComplete = true };
        //Todo todo4 = new Todo() { TodoId = 4, Title = "Update calendar", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(4)), IsImportant = false, IsComplete = false };
        //Todo todo5 = new Todo() { TodoId = 5, Title = "Create slides", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-10)), IsImportant = true, IsComplete = true };
        //Todo todo6 = new Todo() { TodoId = 6, Title = "Call recruiter", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)), IsImportant = false, IsComplete = false };
        //Todo todo7 = new Todo() { TodoId = 7, Title = "Email sales, they did a great job this quarter", DueDate = null, IsImportant = false, IsComplete = false };
        //Todo todo8 = new Todo() { TodoId = 8, Title = "Check on sky miles", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(10)), IsImportant = true, IsComplete = false };
        //Todo todo9 = new Todo() { TodoId = 9, Title = "Look for airbnb", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-14)), IsImportant = false, IsComplete = false };
        //Todo todo10 = new Todo() { TodoId = 10, Title = "Shop for new shoes", DueDate = null, IsImportant = false, IsComplete = false };
        //Todo todo11 = new Todo() { TodoId = 11, Title = "Clear schedule for ten days!", DueDate = null, IsImportant = false, IsComplete = false };
        //Todo todo12 = new Todo() { TodoId = 12, Title = "Dishes", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7)), IsImportant = false, IsComplete = true };
        //Todo todo13 = new Todo() { TodoId = 13, Title = "Clean fridge", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-3)), IsImportant = false, IsComplete = false };
        //Todo todo14 = new Todo() { TodoId = 14, Title = "Mop floor", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)), IsImportant = false, IsComplete = false };
        //Todo todo15 = new Todo() { TodoId = 15, Title = "Walk dog", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-7)), IsImportant = true, IsComplete = false };
        //Todo todo16 = new Todo() { TodoId = 16, Title = "Find recipe", DueDate = null, IsImportant = false, IsComplete = false };
        //Todo todo17 = new Todo() { TodoId = 17, Title = "Find out if the weird coin I found is rare and valuable, or just some hunk of crap.", DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(14)), IsImportant = false, IsComplete = false };

        //Todo[] group1 = [todo1, todo2, todo3];
        //Todo[] group2 = [todo4, todo5, todo6, todo7];
        //Todo[] group3 = [todo8, todo9, todo10, todo11];
        //Todo[] group4 = [todo12, todo13, todo14];
        //Todo[] group5 = [todo15, todo16, todo17];

        //list1.Todos = group1;
        //list2.Todos = group2;
        //list3.Todos = group3;
        //list4.Todos = group4;
        //list5.Todos = group5;

        //return [list1, list2, list3, list4, list5];
        return [];
    }
}
