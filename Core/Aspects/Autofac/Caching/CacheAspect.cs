using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect: MethodInterception //REPEAT THIS PART : https://www.udemy.com/course/net-core-c-sharp-kursu-2/learn/lecture/16396324#questions/8583358
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();

        }
        
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
            }
            invocation.Proceed();
            _cacheManager.Add(key,invocation.ReturnValue,_duration);
        }
    }
}
