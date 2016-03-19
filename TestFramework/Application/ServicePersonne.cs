using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core;
using MKS.Core.Business;

namespace TestFramework.Application
{
    public class ServicePersonne
    {
        public Personne Ajouter(Personne personne)
        {
            MKSBusiness<Personne> b = new MKSBusiness<Personne>();
            
            b.SetValidation(new ValidationPersonne());
            b.SetPreProcessAdd(new ProcessAddPersonne());
            

            return b.Add(personne);
        }
        public List<Personne> SelectList(SearchPersonne searchp)
        {
            MKSBusiness<Personne, List<Personne>, SearchPersonne> b = new MKSBusiness<Personne, List<Personne>, SearchPersonne>();
            b.SetValidationSearch(new ValidationSearchPersonne());


            return b.Select(searchp);
        }
    }
}
