using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IRepository;
using DAL.Models;

namespace DAL.Repository
{
    [Serializable]
    public class UserRepository : IUserRepository
    {
        public Person GetPersonBy(int id)
        {
            using (DBContext context = new DBContext())
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
            using (DBContext context = new DBContext())
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
