// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Container.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CachingFramework.Core.Container
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CachingFramework.Core.Interface;

    using Ninject;
    using Ninject.Extensions.Conventions;

    /// <summary>
    ///     Container class wrapping <see cref="UnityContainer" />
    /// </summary>
    /// <remarks>
    ///     Resolves based on the first registration to the container, so registration can be
    ///     overridden at runtime using the <see cref="UnityConfigurationSection" /> settings
    /// </remarks>
    public static class Container
    {
        /// <summary>
        /// </summary>
        private static IKernel _kernel;

        /// <summary>
        /// </summary>
        static Container()
        {
            Initialise();
        }

        /// <summary>
        /// </summary>
        private static void Initialise()
        {
            _kernel = new StandardKernel();
            var thisAssembly = typeof(ICache).Assembly;
            RegisterAll<CacheBase>(Lifetime.Transient);

            // caches:
        }

        /// <summary>
        /// </summary>
        /// <param name="type">
        /// </param>
        /// <returns>
        /// </returns>
        private static bool IsServiceType(Type type)
        {
            return type.IsClass && type.GetInterfaces().Any(intface => intface.Name == "I" + type.Name);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public static IEnumerable<T> GetAll<T>() where T : class
        {
            var implementations = new List<T>();
            implementations.AddRange(_kernel.GetAll<T>());

            // if there are no results, check for a single registration:
            if (implementations.Count == 0)
            {
                var implementation = Get<T>();
                if (implementation != null)
                {
                    implementations.Add(implementation);
                }
            }

            return implementations;
        }

        /// <summary>
        /// Register all implementations of a type in the given assembly
        /// </summary>
        /// <remarks>
        /// Registers all implementations - so allows multiple registrations per type
        /// </remarks>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="scope">
        /// </param>
        private static void RegisterAll<T>(Lifetime scope)
        {
            RegisterFromThisAssembly<T>(scope);
        }

        /// <summary>
        /// </summary>
        /// <param name="scope">
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        private static void RegisterFromThisAssembly<T>(Lifetime scope)
        {
            switch (scope)
            {
                case Lifetime.Singleton:
                    _kernel.Bind(
                        x =>
                        x.FromThisAssembly()
                            .SelectAllClasses()
                            .InheritedFrom<T>()
                            .BindBase()
                            .Configure(c => c.InSingletonScope()));
                    break;
                case Lifetime.Transient:
                    _kernel.Bind(
                        x =>
                        x.FromThisAssembly()
                            .SelectAllClasses()
                            .InheritedFrom<T>()
                            .BindBase()
                            .Configure(c => c.InTransientScope()));
                    break;
                case Lifetime.Thread:
                    _kernel.Bind(
                        x =>
                        x.FromThisAssembly()
                            .SelectAllClasses()
                            .InheritedFrom<T>()
                            .BindBase()
                            .Configure(c => c.InThreadScope()));
                    break;
                default:
                    _kernel.Bind(
                       x =>
                       x.FromThisAssembly()
                           .SelectAllClasses()
                           .InheritedFrom<T>()
                           .BindBase()
                           .Configure(c => c.InSingletonScope()));
                    break;
            }
        }

        /// <summary>
        /// Retrieve the default implementation of a registered type
        /// </summary>
        /// <returns>
        /// </returns>
        private static T Get<T>()
        {
            var implementation = default(T);
            try
            {
                implementation = _kernel.Get<T>();
            }
            catch (Exception ex)
            {
            }

            return implementation;
        }
    }

    /// <summary>
    ///     Object lifetime for container resolution
    /// </summary>
    public enum Lifetime
    {
        /// <summary>
        ///     New object instance created on every resolution
        /// </summary>
        Transient,

        /// <summary>
        ///     Single object used for every resolution within an App Domain
        /// </summary>
        Singleton,

        /// <summary>
        ///     Single object used for every resolution within a thread
        /// </summary>
        Thread
    }
}