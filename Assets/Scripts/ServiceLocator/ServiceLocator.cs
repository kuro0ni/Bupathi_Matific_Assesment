using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private Dictionary<Service, IGameService> Services = new Dictionary<Service, IGameService>();

    public static ServiceLocator Current { get; private set; }

    public static void Initiailze()
    {
        Current = new ServiceLocator();
    }

    public T Get<T>(Service serviceType)
    {
        if (!Services.ContainsKey(serviceType))
        {
            Debug.LogError($"{serviceType} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        }

        return (T)Services[serviceType];
    }

    public void Register<T>(T service, Service serviceType) where T : IGameService
    {
        if (Services.ContainsKey(serviceType))
        {
            Debug.LogError($"Attempted to register service of type {serviceType} which is already registered with the {GetType().Name}.");
            return;
        }

        Services.Add(serviceType, service);
    }

    public void Unregister<T>(Service serviceType) where T : IGameService
    {
        if (!Services.ContainsKey(serviceType))
        {
            Debug.LogError($"Attempted to unregister service of type {serviceType} which is not registered with the {GetType().Name}.");
            return;
        }

        Services.Remove(serviceType);
    }
}

public interface IGameService {}

public enum Service
{
    USER_DATA_GETTER,
    COSMETIC_DATA_GETTER,
    COSMETIC_MANAGER,
    AUDIO_CONTROLLER
}
