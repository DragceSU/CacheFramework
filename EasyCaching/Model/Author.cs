// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Author.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace EasyCaching.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// </summary>
    [Serializable]
    public class Author
    {
        /// <summary>
        /// </summary>
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        public virtual ICollection<Book> Books { get; set; }
    }
}