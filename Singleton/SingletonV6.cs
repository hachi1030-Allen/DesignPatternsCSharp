using System;


namespace Singleton
{
    // Sixth edition - using .NET 4's Lazy<T> type

    /// <summary>
    /// It's simple and performs well. It also allows you to check whether or not the instance has been created yet with the IsValueCreated property, if you need that.
    /// 
    /// The code above implicitly uses LazyThreadSafetyMode.ExecutionAndPublication 
    /// as the thread safety mode for the Lazy<Singleton>. Depending on your requirements, you may wish to experiment with other modes.
    /// </summary>
    public sealed class SingletonV6
    {
        private static readonly Lazy<SingletonV6> lazy = new Lazy<SingletonV6>(() => new SingletonV6());

        public static SingletonV6 Instace {get {return lazy.Value;}}

        private SingletonV6() {}
    }
}