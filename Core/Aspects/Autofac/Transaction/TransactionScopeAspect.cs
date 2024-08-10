using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Core.Aspects.Autofac.Transaction
{
    //this aspect helps manage transactions automatically by ensuring that a method's operations are either fully completed or fully rolled back.
    //This is crucial for maintaining data integrity, especially in systems that perform multiple related database operations within a single method.

    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            //If any operation fails, the entire transaction is rolled back, meaning all changes made during the transaction are undone.
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
