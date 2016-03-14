using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TestFramework.Application;

namespace TestFramework
{
    [TestClass]
    public class InjectionDependance
    {
        [TestMethod]
        public void TestMethod1()
        {
            ServicePersonne s = new ServicePersonne();
            Personne p=new Personne();
            p.Nom="KATEE";
            p = s.Ajouter(p);

            
        }
    }
}
