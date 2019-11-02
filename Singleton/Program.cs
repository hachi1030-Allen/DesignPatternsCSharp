using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var singletonIns = SingletonV4.Instance;

            Console.WriteLine(singletonIns.GetHashCode().ToString());

            Console.ReadLine();
        }
    }
}
