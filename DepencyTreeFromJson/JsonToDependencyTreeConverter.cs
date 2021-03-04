using Newtonsoft.Json;
using System;
using System.Linq;
using TransitiveDependencyResolver.model;

namespace DepencyTreeFromJson
{
    public class JsonToDependencyTreeConverter : IJsonToDependencyTreeConverter
    {
        public IDependencyTree<T> Convert<T>(string json, Func<string, T> mapFromString)
        {
            var dependencyTree = new DependencyTree<T>();
            var dependencyTreeFromJson = JsonConvert.DeserializeObject<dependencyTree>(json);

            foreach (var branch in dependencyTreeFromJson.branches)
            {
                dependencyTree.Add(mapFromString(branch.module), branch.dependencies.Select(mapFromString).ToArray());
            }

            return dependencyTree;
        }
    }
}
