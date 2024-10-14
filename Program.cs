using System.Reflection;
using Dumpify;

internal class Program
{
    private static void Main(string[] args)
    {
        typeof(ServiceA).GetConstructors()
            .First()
            .GetParameters()
            .Select(param =>
            {
                return new { param.Name, param.ParameterType };
            })
            .Dump();
    }
}

public class ServiceA
{
    public ServiceA(string a)
    {
    }
}
