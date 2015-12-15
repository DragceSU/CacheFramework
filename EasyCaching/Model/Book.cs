// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Book.cs" company="">
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
    public class Book
    {
        /// <summary>
        /// </summary>
        public Book()
        {
            this.Authors = new HashSet<Author>();
        }

        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        public virtual ICollection<Author> Authors { get; set; }
    }
}