using Ecom.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Horrid Henry",
                Description = "Horrid Henry is the first book of the Horrid Henry series. It was published in 1994 and written by Francesca Simon and illustrated by Tony Ross. The book is a collection of short stories about the same characters, along the lines of the Just William books.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/81/Horrid_Henry_1_%28first_paperback_edition%29.jpg",
                Price = 9.99m
            },
            new Product
            {
                Id = 2,
                Title = "Harry Potter and the Philosopher's Stone",
                Description = "Harry Potter and the Philosopher's Stone is a 1997 fantasy novel written by British author J. K. Rowling. The first novel in the Harry Potter series and Rowling's debut novel, it follows Harry Potter, a young wizard who discovers his magical heritage on his eleventh birthday, when he receives a letter of acceptance to Hogwarts School of Witchcraft and Wizardry. Harry makes close friends and a few enemies during his first year at the school and with the help of his friends, he faces an attempted comeback by the dark wizard Lord Voldemort, who killed Harry's parents, but failed to kill Harry when he was just 15 months old.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg",
                Price = 12.99m
            },
            new Product
            {
                Id = 3,
                Title = "The Lord of the Rings",
                Description = "The Lord of the Rings is an epic high-fantasy novel[a] by English author and scholar J. R. R. Tolkien. Set in Middle-earth, intended to be Earth at some time in the distant past, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work. Written in stages between 1937 and 1949, The Lord of the Rings is one of the best-selling books ever written, with over 150 million copies sold.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif",
                Price = 7.99m
            }

           );
        }

        public DbSet<Product> Products { get; set; }
    }
}
