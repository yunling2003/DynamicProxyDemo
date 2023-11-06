using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicProxyDemo
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddService<TInterface, TImp>(this IServiceCollection services,
                                                        ServiceLifetime lifetime, 
                                                        params Type[] interceptorTypes)
        {
            return services.AddService(typeof(TInterface), typeof(TImp), lifetime, interceptorTypes); 
        }

        public static IServiceCollection AddService(this IServiceCollection services,
                                                    Type serviceType, 
                                                    Type implType,
                                                    ServiceLifetime lifetime,
                                                    params Type[] interceptorTypes)
        {
            services.Add(new ServiceDescriptor(implType, implType, lifetime));

            Func<IServiceProvider, object> factory = (provider) =>
            {
                var target = provider.GetService(implType);

                var interceptors = interceptorTypes.ToList().ConvertAll<IInterceptor>(interceptorType =>
                {
                    return provider.GetService(interceptorType) as IInterceptor;
                });

                return new ProxyGenerator().CreateInterfaceProxyWithTarget(serviceType, target, interceptors.ToArray());
            };

            var serviceDescriptor = new ServiceDescriptor(serviceType, factory, lifetime);
            services.Add(serviceDescriptor);

            return services;
        }
    }
}
