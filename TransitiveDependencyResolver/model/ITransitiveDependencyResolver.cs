namespace TransitiveDependencyResolver.model
{
    public interface ITransitiveDependencyResolver
    {
        IDependencyTree<T> Resolve<T>(IDependencyTree<T> dependencyTree);
    }
}