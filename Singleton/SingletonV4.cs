using System;

namespace Singleton
{
    // Thread-safe without using locks.

    /// <summary>
    /// This imlementation is using the static constructor which will be
    /// less lazy but thread safe. Static constructor will make sure that
    /// the instance of the class being generated when the first thread
    /// comes to access this class instance.
    /// </summary>

    public sealed class SingletonV4
    {
        private static readonly SingletonV4 instance = null;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        private SingletonV4() 
        {

        }

        static SingletonV4() 
        {
            instance = new SingletonV4();
        }

        public static SingletonV4 Instance
        {
            get 
            {
                return instance;
            }
        }
    }
}