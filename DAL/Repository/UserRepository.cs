using System;
using System.Linq;
using DAL.IRepository;
using DAL.Models;

namespace DAL.Repository
{
    [Serializable]
    public class UserRepository : IUserRepository
    {
        public Person GetPersonBy(int id)
        {
            using (var context = new DBContext())
            {
                try
                {
                    return context.People.FirstOrDefault(p => p.BusinessEntityID == id);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public Person GetPersonBy(string firstName, string lastName)
        {
            using (var context = new DBContext())
            {
                try
                {
                    return context.People.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}