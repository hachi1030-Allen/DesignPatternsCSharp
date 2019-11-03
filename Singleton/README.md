# Implementation of Singleton Pattern in C#

## Table of Contents
- [Introduction](#Introduction)
- [Non-thread-safe version](#First version - not thread-safe)
- [c](#c)

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

## c