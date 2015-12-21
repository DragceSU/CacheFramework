// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookRepository.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace EasyCaching.Model
{
    using System;
    using System.Data.Entity;

    /// <summary>
    /// </summary>
    [Serializable]
    internal class BookRepository : DbContext
    {
        /// <summary>
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// </summary>
        public DbSet<Author> Authors { get; set; }
    }
}