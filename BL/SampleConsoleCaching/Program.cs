// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SampleConsoleCaching
{
    using System;
    using System.Linq;

    using Container;

    using DAL.Repository;

    using Ninject;

    using SampleConsoleCaching.APIs;

    /// <summary>
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// </summary>
        /// <param name="args">
        /// </param>
        private static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            InitializeContainer.Register(kernel);

            var userRepository = kernel.Get<UserRepository>();
            var bookAPI = new PersonApi(userRepository);

            foreach (var i in Enumerable.Range(0, 10))
            {
                var start = DateTime.Now;

                // BookApi.GetBooks("Ross L. Finney").ToList();
                bookAPI.GetPerson("David", "Bradley");
                bookAPI.GetPerson("Michael", "Raheem");
                bookAPI.GetPerson("Kevin", "Brown");
                var end = DateTime.Now;
                Console.WriteLine(
                    "Iteration {0} completed within: {1} miliseconds for a single cached object.", 
                    i + 1, 
                    (end - start).TotalMilliseconds);

                start = DateTime.Now;

                // BookApi.GetBooks("Ross L. Finney").ToList();
                bookAPI.GetAllPersons();
                end = DateTime.Now;
                Console.WriteLine(
                    "Iteration {0} completed within: {1} miliseconds for a list of cached objects.", 
                    i + 1, 
                    (end - start).TotalMilliseconds);

                // Uncommenting the line below causes the cached data to be invalidated
                // in each iteration, hence raising the exceution time of each iteration
                // to more than 1 second.
                // BookApi.ChangeAuthorName("a", "b");
            }
        }
    }
}