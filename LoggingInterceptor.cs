using Castle.DynamicProxy;

namespace DynamicProxyDemo
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            Console.WriteLine($"Begin to execute method: {methodName}...");

            invocation.Proceed();

            Console.WriteLine($"Method: {methodName} executed..." );
        }
    }
}
