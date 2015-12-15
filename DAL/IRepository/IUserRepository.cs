using DAL.Models;

namespace DAL.IRepository
{
    public interface IUserRepository
    {
        Person GetPersonBy(int id);
        Person GetPersonBy(string firstName, string lastName);
    }
}