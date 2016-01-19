using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Activity;

namespace MKS.Core.Presenter
{
    public abstract class JournalisationAdapter:IActivityAdapter
    {
        public IActivity _ev = new ActivityLocal();

        public IActivity Activity
        {
            get
            {
                return (_ev);
            }
            set
            {
                _ev = value;
            }
        }

        public virtual void DoActivityLog(IActivity eventlog)
        {
            //Appel au service de journalisation
        }
    }
}
