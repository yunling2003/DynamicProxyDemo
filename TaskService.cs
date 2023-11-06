namespace DynamicProxyDemo
{
    public class TaskService : ITaskService
    {
        public void Run()
        {
            Console.WriteLine("Task is running...");
        }
    }
}
