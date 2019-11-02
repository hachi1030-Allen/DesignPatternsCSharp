using System;

namespace Singleton
{
    public sealed class SingletonV4
    {
        private static readonly SingletonV4 instance = null;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonV4() 
        {
            instance = new SingletonV4();
        }

        private SingletonV4() 
        {

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