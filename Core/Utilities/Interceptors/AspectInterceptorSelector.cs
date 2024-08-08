using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector:IInterceptorSelector
    {
        // This class is responsible for selecting and ordering interceptors for Autofac
        // and Castle DynamicProxy's proxy generation to enabling aspect-oriented programming (AOP) with our aspects.
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            classAttributes.AddRange(methodAttributes);

            //This is explained in "MethodInterceptionBaseAttribute"
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
