using System;

namespace Singleton
{
    // Fifth edition - fully lazy instantiation

    /// <summary>
    /// Instantiation is triggered by the first reference to the static member of the nested class, which only occurs in Instance.
    /// This means the implementation is fully lazy, but has all the performance benefits of the previous ones. 
    /// </summary>
    public sealed class SingletonV5
    {
        public static SingletonV5 Instance {get {return Nested.instance;}}

        private class Nested
        {
            static Nested() {}
            internal static readonly SingletonV5 instance = new SingletonV5();
        }
    }
}