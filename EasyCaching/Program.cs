using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using EasyCaching.APIs;
using EasyCaching.Model;
using Ninject;

namespace EasyCaching
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dump call to create Entity Framework model.
            //new BookRepository().Books.ToList();
            var kernel = new StandardKernel();
            Container.InitializeContainer.Register(kernel); 

            var userRepository = kernel.Get<UserRepository>();
            var bookAPI = new BookApi(userRepository);

            foreach (var i in Enumerable.Range(0, 10))
            {
                var start = DateTime.Now;
                //BookApi.GetBooks("Ross L. Finney").ToList();
                bookAPI.GetPerson("David", "Bradley");
                bookAPI.GetPerson("Michael", "Raheem");
                bookAPI.GetPerson("Kevin", "Brown");
                var end = DateTime.Now;
                Console.WriteLine((end - start).TotalMilliseconds);

                // Uncommenting the line below causes the cached data to be invalidated
                // in each iteration, hence raising the exceution time of each iteration
                // to more than 1 second.
                //BookApi.ChangeAuthorName("a", "b");
            }
        }
    }
}
