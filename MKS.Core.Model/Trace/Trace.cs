using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Core.Model
{
    public class Trace
    {
        public string FunctionCode { get; set; }
        public string ProcessCode { get; set; }
        public DateTime Date { get; set; }
        public string UserCode { get; set; }
        public string Titre { get; set; }
        public string Informations { get; set; }        
        public string Value { get; set; }
        public string Computer { get; set; }
        public string AppCode { get; set; }
        public string GUID { get; set; }
    }
}
