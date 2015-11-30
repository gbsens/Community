using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Business;

namespace MKS.Core.Presenter
{
    public class BusinessError:BusinessProcessError
    {
        public override void BusinessProcessException(BusinessObjectError businessObject, Exception exception)
        {
            base.BusinessProcessException(businessObject, exception);
        }
        public override void BusinessProcessValidation(BusinessObjectError businessObject, Model.Error.ExceptionProcess<ProcessResults> ExceptionProcess)
        {
            
        }
    }
}
