using NUnit.Framework;
using System;
using System.Transactions;

namespace JamCentral.IntegrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        void ITestAction.BeforeTest(TestDetails testDetails)
        {
            _transactionScope = new TransactionScope();
        }

        void ITestAction.AfterTest(TestDetails testDetails)
        {
            _transactionScope.Dispose();
        }
        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}
