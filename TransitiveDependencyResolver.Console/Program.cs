using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DepencyTreeFromJson;
using System;
using System.IO;
using TransitiveDependencyResolver.model;

namespace TransitiveDependencyResolver.Console
{
    class Program
    {
        private static WindsorContainer _container;

        static void Main(string[] args)
        {
            InitializeContainer();

            var transitiveDependencyResolver = _container.Resolve<ITransitiveDependencyResolver>();
            var jsonToDependencyTreeConverter = _container.Resolve<IJsonToDependencyTreeConverter>();

            ResolveGuidDependencyTree(transitiveDependencyResolver, jsonToDependencyTreeConverter);
            ResolveStringDependencyTree(transitiveDependencyResolver, jsonToDependencyTreeConverter);
        }

        private static void ResolveGuidDependencyTree(ITransitiveDependencyResolver transitiveDependencyResolver, IJsonToDependencyTreeConverter jsonToDependencyTreeConverter)
        {
            var guidDependencyTree = jsonToDependencyTreeConverter.Convert(File.ReadAllText(@"examples\projectGuids.json"), x => new Guid(x));
            var transistiveDependencyTree = transitiveDependencyResolver.Resolve(guidDependencyTree);
        }

        private static void ResolveStringDependencyTree(ITransitiveDependencyResolver transitiveDependencyResolver, IJsonToDependencyTreeConverter jsonToDependencyTreeConverter)
        {
            var stringDependencyTree = jsonToDependencyTreeConverter.Convert(File.ReadAllText(@"examples\projectNames.json"), x => x);
            var transistiveDependencyTree = transitiveDependencyResolver.Resolve(stringDependencyTree);
        }

        private static void InitializeContainer()
        {
            _container = new WindsorContainer();

            RegisterComponent<IJsonToDependencyTreeConverter, JsonToDependencyTreeConverter>();
            RegisterComponent<ITransitiveDependencyResolver, TransitiveDependencyResolverImplementation>();
        }

        private static void RegisterComponent<TService, TImpl>() 
            where TService : class 
            where TImpl : TService
        {
            _container.Register(Component
                .For<TService>()
                .ImplementedBy<TImpl>());
        }
    }
}