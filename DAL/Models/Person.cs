// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// </summary>
    [Table("Person.Person")]
    [Serializable]
    public class Person
    {
        /// <summary>
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BusinessEntityID { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        [StringLength(2)]
        public string PersonType { get; set; }

        /// <summary>
        /// </summary>
        public bool NameStyle { get; set; }

        /// <summary>
        /// </summary>
        [StringLength(8)]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// </summary>
        [StringLength(50)]
        public string MiddleName { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// </summary>
        [StringLength(10)]
        public string Suffix { get; set; }

        /// <summary>
        /// </summary>
        public int EmailPromotion { get; set; }

        /// <summary>
        /// </summary>
        [Column(TypeName = "xml")]
        public string AdditionalContactInfo { get; set; }

        /// <summary>
        /// </summary>
        [Column(TypeName = "xml")]
        public string Demographics { get; set; }

        /// <summary>
        /// </summary>
        public Guid rowguid { get; set; }

        /// <summary>
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}