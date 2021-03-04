using System;
using TransitiveDependencyResolver.model;

namespace DepencyTreeFromJson
{
    public interface IJsonToDependencyTreeConverter
    {
        IDependencyTree<T> Convert<T>(string json, Func<string, T> mapFromString);
    }
}
