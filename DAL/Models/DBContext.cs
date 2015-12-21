// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBContext.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.Models
{
    using System.Data.Entity;

    /// <summary>
    /// </summary>
    public class DBContext : DbContext
    {
        /// <summary>
        /// </summary>
        public DBContext()
            : base("name=DBContext")
        {
        }

        /// <summary>
        /// </summary>
        public virtual DbSet<Person> People { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="modelBuilder">
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(e => e.PersonType).IsFixedLength();
        }
    }
}