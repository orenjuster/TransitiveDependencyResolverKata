using DepencyTreeFromJson;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using TransitiveDependencyResolver.model;

namespace TransitiveDependencyResolver.Test
{
    [TestClass]
    public class TransitiveDependencyResolverImplementationTest
    {
        [TestMethod]
        public void FullTest()
        {
            var transitiveDependencyResolver = new TransitiveDependencyResolverImplementation();
            var stringDependencyTree = new JsonToDependencyTreeConverter().Convert(File.ReadAllText(@"examples\projectNames.json"), x => x);
            var result = transitiveDependencyResolver.Resolve(stringDependencyTree);
            
            var expected = new JsonToDependencyTreeConverter().Convert(File.ReadAllText(@"examples\finalResult.json"), x => x);
            
            AssertExpectedOutput(result, expected);
        }

        private void AssertExpectedOutput(IDependencyTree<string> result, IDependencyTree<string> expected)
        {
            CollectionAssert.AreEquivalent(expected.GetAllModules().ToArray(), result.GetAllModules().ToArray());
            
            foreach (var resultModule in result.GetAllModules())
            {
                CollectionAssert.AreEquivalent(expected.GetDependenciesOf(resultModule).ToArray(), result.GetDependenciesOf(resultModule).ToArray());
            }
        }
    }
}
