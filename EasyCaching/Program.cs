using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCaching.APIs;
using EasyCaching.Model;

namespace EasyCaching
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dump call to create Entity Framework model.
            //new BookRepository().Books.ToList();

            foreach (var i in Enumerable.Range(0, 10))
            {
                var start = DateTime.Now;
                //BookApi.GetBooks("Ross L. Finney").ToList();
                BookApi.GetPerson("David", "Bradley");
                BookApi.GetPerson("Michael", "Raheem");
                BookApi.GetPerson("Kevin", "Brown");
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
