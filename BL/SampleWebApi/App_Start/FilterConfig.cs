// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SampleWebApi
{
    using System.Web.Mvc;

    /// <summary>
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// </summary>
        /// <param name="filters">
        /// </param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}