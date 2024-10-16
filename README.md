# Dependency Injection

## Definition
**Dependency injection is a way to achieve Dependency Inversion Principle** in software development.
It will inject the defined implementation whenever something needed it.

## Dependency Inversion Principle

- Regularly, when we need to use some methods of service B inside of service A, programmer need to initiate
service B and pass it into service A via constructor. It make the service A is strictly depended on service B.
```
Service A -> Service B
```

- To decouple it, both of them should depend on abstraction layer
```
Service A -> Abstraction B <- Service B
```

## Lifespan

- With dependency injection, we have 3 lifespan types:
    - Transient: Objects are **always different**
    - Scoped: Objects are the same **within a request**
    - Singleton: the same **single instance** is used for all of injection.

## How to implement DI Provider
- Using `System.Reflection` to dynamically interract with class | interface.
- Using `Activator.CreateInstance<T>()` to dynamically create an new instance.
- Using `typeof(class | interface)` to get a Type of class or interface.
- class `Type`:
    - Using function `GetConstructors` to get list of constructors defined inside a class
    - Using function `GetParameters` of ConstructorInfo to get list paramters defined inside a single constructor.
