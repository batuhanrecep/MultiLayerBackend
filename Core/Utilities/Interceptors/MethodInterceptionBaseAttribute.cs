using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    //It can be used on top of the class, inherited classes, methods and if it's needed more than once
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)] 
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; } // I use this to set the aspects order.(Usually, first one is working first but for anyway it's the better way)
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
