using System.Collections.Generic;
using System.Linq;

namespace TransitiveDependencyResolver.model
{
    public class DependencyTree<T> : IDependencyTree<T>
    {
        private readonly IDictionary<T, List<T>> _dictionary;

        public DependencyTree()
        {
            _dictionary = new Dictionary<T, List<T>>();
        }

        public void Add(T module, params T[] dependencies)
        {
            if (!_dictionary.ContainsKey(module))
                _dictionary.Add(module, new List<T>());

            _dictionary[module].AddRange(dependencies);
        }

        public IList<T> GetAllModules()
        {
            return _dictionary.Keys.ToList();
        }

        public IList<T> GetDependenciesOf(T module)
        {
            return _dictionary[module];
        }
    }
}