using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ServiceLocatorInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initiailze()
    {
        ServiceLocator.Initiailze();

        UserDataGetter userDataGetter = new UserDataGetter();
        UserStatManager userStatManager = new UserStatManager(userDataGetter);

        ServiceLocator.Current.Register(userDataGetter, Service.USER_DATA_GETTER);
        ServiceLocator.Current.Register(userStatManager, Service.USER_STAT_MANAGER);

        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
