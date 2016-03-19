using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MKS.Core.Business;
using TestFramework.Application;
using System.Collections.Generic;

namespace TestFramework
{
    [TestClass]
    public class Validation
    {
        [TestMethod]
        public void ValidationPersoneTest()
        {
            MKSBusiness<Personne> b = new MKSBusiness<Personne>();
            b.SetValidation(new ValidationPersonne());
            Personne p = new Personne();
            p.Nom = "LOLO";
            b.Select(p);
        }
        [TestMethod]
        public void ValidationSearchPersoneTest()
        {
            MKSBusiness<Personne, List<Personne>, SearchPersonne> b = new MKSBusiness<Personne, List<Personne>, SearchPersonne>();
            b.SetValidationSearch(new ValidationSearchPersonne());


            Personne searchpp = new Personne();
            ServicePersonne sp = new ServicePersonne();
            SearchPersonne searchp = new SearchPersonne();
            searchp.Nom = "LOLO";
            try
            {
                b.Select(searchp);
            }
            catch(Exception ex)
            {
                Assert.IsInstanceOfType(ex,typeof( MKS.Core.Model.Error.ExceptionProcess<MKS.Core.ProcessResults>));
            }

        }
        [TestMethod]
        public void ValidationSearchPersoneFromPresenterTest()
        {
            FormPersonne f = new FormPersonne();
            PresenterPersonne p = new PresenterPersonne(f);
       
            p.Start();
            
            //TEST les erreurs dans le l'initialisation du presenter.

            Assert.IsNotNull(f.ViewLogics.ContextValidationMessage);
            
            
        }
        [TestMethod]
        public void ValidationSearchPersoneFromPresenterExecuteCommandTest()
        {
            FormPersonne f = new FormPersonne();
            PresenterPersonne2 p = new PresenterPersonne2(f);
            p.Start();
            p.ExecuteCommand("Test");

            Assert.IsNotNull(f.ViewLogics.ContextValidationMessage);
        }
    }
}
