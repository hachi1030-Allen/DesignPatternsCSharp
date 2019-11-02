using System;

namespace Singleton
{
    // First version - Lazy load and not thread-safe
    public sealed class SingletonV1
    {
        /// <summary>
        /// First version - Lazy load and not thread-safe
        /// This version is bad code, usually should not use this version
        /// It's simply check whether the instance is null, and if yes,
        /// then instantiate the class and return.
        /// If in a multi-thread environment, this is not thread-safe at all.
        /// </summary>
        private static SingletonV1 instance = null;
        private SingletonV1(){}
        public static SingletonV1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonV1();
                }
                return instance;
            }
        }
    }
}