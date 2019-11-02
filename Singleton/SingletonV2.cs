using System;

namespace Singleton
{
    // Second version - simple thread safety

    /// <summary>
    /// This version is the simplest version that implemented
    /// synchronization which ensures thread-safety.
    /// We use a lock object to do the synchronization so that
    /// it can make sure only one thread can create the object.
    /// 
    /// Downside: This implementation will have performance issue
    /// becuase lock will always spend resource, and in multi-thread environment,
    /// this implementation will casue lots of threads to wait for other threads to
    /// free the lock
    /// </summary>
    public sealed class SingletonV2
    {
        private static SingletonV2 instance = null;
        private static readonly object padlock = new object();
        // Private constructor
        private SingletonV2() {}

        public static SingletonV2 Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SingletonV2();
                    }
                    return instance;
                }
            }
        }
    }
}