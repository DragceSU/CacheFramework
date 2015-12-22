// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonController.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SampleWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web.Http;
    using System.Web.Http.Results;

    using DAL.IRepository;
    using DAL.Models;

    using SampleWebApi.Services;

    /// <summary>
    /// </summary>
    // [Authorize]
    [RoutePrefix("api")]
    public class PersonController : ApiController
    {
        /// <summary>
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// </summary>
        /// <param name="userRepository">
        /// </param>
        public PersonController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        // GET api/values
        /// <summary>
        /// </summary>
        /// <param name="firstName">
        /// </param>
        /// <param name="lastName">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        [Route("person/{firstName}/{lastName}")]
        public JsonResult<Person> GetPerson(string firstName, string lastName)
        {
            return this.Json(new UserService(this._userRepository).GetPerson(firstName, lastName));
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        [Route("person/getall")]
        public JsonResult<List<Person>> GetAllPersons()
        {
            return this.Json(new UserService(this._userRepository).GetAllPersons().Take(10).ToList());
        }

        /// <summary>
        /// </summary>
        /// <param name="oldName">
        /// </param>
        /// <param name="oldLastName">
        /// </param>
        /// <param name="newName">
        /// </param>
        /// <param name="newLastName">
        /// </param>
        /// <param name="personObj"></param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Route("upload")]
        public JsonResult<bool> ChangeNameBy(UpdatePerson personObj)
        {
            return
                this.Json(
                    new UserService(this._userRepository).ChangeNameBy(personObj.oldName, personObj.oldLastName, personObj.newName, personObj.newLastName));
        }

        
    }

    [DataContract]
    [Serializable]
    public class UpdatePerson
    {
        [DataMember]
        public string oldName { get; set; }
        [DataMember]
        public string oldLastName { get; set; }
        [DataMember]
        public string newName { get; set; }
        [DataMember]
        public string newLastName { get; set; }
    }
}