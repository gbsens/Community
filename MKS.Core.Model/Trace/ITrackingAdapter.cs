using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Core.Model
{
    public interface ITrackingAdapter:IDisposable
    {
        void Add(Trace trace);
    }
}
