using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

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
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); //To automatically execute our aspect.
            //classAttributes.Add(new ExceptionLogAspect(typeof(DatabaseLogger))); // both can be work together 


            //This is explained in "MethodInterceptionBaseAttribute"
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
