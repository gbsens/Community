using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MKS.Core;

namespace TestFramework
{
    [TestClass]
    public class ErrorLog
    {
        [TestMethod]
        public void PublishTest()
        {
            MKS.Library.ErrorLog.PublishExceptionMessage(new Exception("TEST"), Globals.GetUserEnvironment);
        }
    }
}
