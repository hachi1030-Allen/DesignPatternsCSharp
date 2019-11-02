using System;

namespace Singleton
{
    // Third version - attemted thread-safety using double-check locking.

    /// <summary>
    /// It's quite strange, actually this pattern is usually used in Java version.
    /// This is marked as bad code, not to use becuase one of its downside it that
    /// it won't work for Java is you implement the same. In Java version, you have
    /// to use the keyword volatile on the instance variable, otherwise it won't be
    /// thread-safe.
    /// 
    /// Double checked locking has an advantage that it has a huge performance
    /// improvement than the V2 version. Because it can avoid lots of the lock
    /// operations becuase of the first null check for the instance. ( Line 29 )
    /// </summary>
    
    // Copied the downsides from the orginal author:
    /*This implementation attempts to be thread-safe without the necessity of taking out a lock every time. Unfortunately, there are four downsides to the pattern:

    1. It doesn't work in Java. This may seem an odd thing to comment on, but it's worth knowing if you ever need the singleton pattern in Java,
       and C# programmers may well also be Java programmers. The Java memory model doesn't ensure that the constructor completes before the 
       reference to the new object is assigned to instance. The Java memory model underwent a reworking for version 1.5, 
       but double-check locking is still broken after this without a volatile variable (as in C#).

    2. Without any memory barriers, it's broken in the ECMA CLI specification too. It's possible that under the .NET 2.0 memory model 
       (which is stronger than the ECMA spec) it's safe, but I'd rather not rely on those stronger semantics, especially if there's any doubt as to the safety. 
       Making the instance variable volatile can make it work, as would explicit memory barrier calls, although in the latter case even 
       experts can't agree exactly which barriers are required. I tend to try to avoid situations where experts don't agree what's right and what's wrong!

    3. It's easy to get wrong. The pattern needs to be pretty much exactly as above - any significant 
       changes are likely to impact either performance or correctness.
       
    4. It still doesn't perform as well as the later implementations. */
    public sealed class SingletonV3
    {
        private static SingletonV3 instance = null;
        private static readonly object padlock = new object();

        private SingletonV3() {}

        public static SingletonV3 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonV3();
                        }
                    }
                }
                return instance;
            }
        }
    }
}