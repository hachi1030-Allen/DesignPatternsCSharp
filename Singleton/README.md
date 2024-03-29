# Implementation of Singleton Pattern in C#

## Table of Contents
- [Introduction](#introduction)
- [Non-thread-safe version](#first-version---not-thread-safe)
- [Simple thread-safe via locking](#second-version---simple-thread-safe)
- [Double-checked locking](#third-version---thread-safe-using-double-checked-locking)
- [Intialize using static constructor](#fourth-version---not-quite-as-lazy-but-thread-safe-without-using-locks)
- [Safe and fully lazy static intialization](#fifth-version---fully-lazy-instantiation)
- [Lazy[T]](#sixth-version---using-net-4s-lazy-type)
- [Conclusion](#conclusion)

## Introduction
Singleton pattern is one of the best-known pattern in software engineering. Essentially, a singleton is a class which only allows a single instance of itself to be created, and usually gives simple access to that instance.

There is actually a very good document from C# in Depth [here](https://csharpindepth.com/Articles/Singleton). Most of the code is implemented same as that document, only V4 is a little bit different. But the explaination is somehow modified by myself which you will be reading in this article.

There can be multiple implementation for Singleton pattern, in this repository, I will use C# and .NET to implement 6 versions of them. But they all meet below design which are actually specialties for Singleton.

- A single constructor, which is private and parameterless. </br>
  Note: This is the most important characteristic for Singleton because Singleton is designed to return only one single instance in the whole application. Therefore, the constructor should be private which means controlled by the class itself. The paramerless for constructor is quite important, assume that you can pass different parameters to the constructor, then you will have different instances. This is another design pattern called Factory`.
- The class is sealed to prevent being inherited.
- A static Property that holds the reference to the single created instance. (This is usually done as a method in Java)

Here are some examples that often used for Singleton:

- Logger
- DB Instance
- Graphic Managers

In this article, we are implementing the `Instance` as a public static property which is commonly used in C# .NET. However, in Java, it's usually being implemented as `getInstance()` method. But you can always make it as a mehtod as well in C# world.

In the repository, I named the Singleton class from V1 to V6, which is actually mapping to the below contents.

## First version - not thread-safe

Code:

```csharp
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
```

The above code is not thread-safe. There is chance that 2 or more different threads have evaluated the test `if (instace == null)` and return ture, then different instances got created which violates the singleton pattern.

## Second version - simple thread-safe

Code:

```csharp
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
```

This implementation is thread-safe. We used object locker to enable the synchronization. However, this implementation is argued that might have performance issue because each time a thread is attempting to get the instance of the class, it will go through the lock and lock is kind of heavy operation.

## Third version - thread-safe using double-checked locking

This is an improved version of the second version because we added one more null check outside the locker, which can avoid lots of the lock operations.

Code:

```csharp
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
```

This is actually quite recommended in Java world, however, this one is not recommended in .NET world due to memory model difference between Java and C#.

For the explaination, I will just copy from C# in Depth document.

- It doesn't work in Java. This may seem an odd thing to comment on, but it's worth knowing if you ever need the singleton pattern in Java, and C# programmers may well also be Java programmers. The Java memory model doesn't ensure that the constructor completes before the reference to the new object is assigned to instance. The Java memory model underwent a reworking for version 1.5, but double-check locking is still broken after this without a `volatile` variable (as in C#).
- Without any memory barriers, it's broken in the ECMA CLI specification too. It's possible that under the .NET 2.0 memory model (which is stronger than the ECMA spec) it's safe, but I'd rather not rely on those stronger semantics, especially if there's any doubt as to the safety. Making the instance variable volatile can make it work, as would explicit memory barrier calls, although in the latter case even experts can't agree exactly which barriers are required. I tend to try to avoid situations where experts don't agree what's right and what's wrong!
- It's easy to get wrong. The pattern needs to be pretty much exactly as above - any significant changes are likely to impact either performance or correctness.
- It still doesn't perform as well as the later implementations.

## Fourth version - not quite as lazy, but thread-safe without using locks

Code:

```csharp
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
```

This implementation instantiate the class in the static constructor.This is thread-safe because static constructor will only be run once when the first thread attempts to access any static member for that class.</br>
How is the lazy then? It's not as lazy as other implementations. Assume that this class has another static member other than `Instance`, the first reference to that member will involve creating the instance. 

## Fifth version - fully lazy instantiation

Code:

```csharp
public sealed class SingletonV5
{
    public static SingletonV5 Instance { get { return Nested.instance; }}

    private class Nested
    {
        static Nested() {}
        internal static readonly SingletonV5 instance = new SingletonV5();
    }
}
```

Here, instantiation is triggered by the first reference to the static member of the nested class, which only occurs in Instance. This means the implementation is fully lazy, but has all the performance benefits of the previous ones.

## Sixth version - using .NET 4's Lazy<T> type

Note: This requires .NET 4.0 or higher.

Code:

```csharp
public sealed class SingletonV6
{
    private static readonly Lazy<SingletonV6> lazy = new Lazy<SingletonV6>(() => new SingletonV6());

    public static SingletonV6 Instace {get {return lazy.Value;}}

    private SingletonV6() {}
}
```

It's simple and performs well. It also allows you to check whether or not the instance has been created yet with the `IsValueCreated` property, if you need that.

The code above implicitly uses `LazyThreadSafetyMode.ExecutionAndPublication` as the thread safety mode for the `Lazy<SingletonV6>`. Depending on your requirements, you may wish to experiment with other modes.

## Conclusion

Definitely choose the thread-safe implementations becuase in modern world, it's not possible to have single thread application especially for web apps.

If you are a starter with .NET, use the 6th version, which is quite simple and safe.

If no need to consider too much about performance, then choose version 2 or 4. (4 is preferred.)

Solution 5 is elegant, but trickier than 2 and 4.