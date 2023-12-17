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

        JSONFileReader jsonFileReader = new JSONFileReader(PathsConfig.COSMETICS_PATH);
        FileStorage fileStorage = new FileStorage(jsonFileReader);
        
        CosmeticDataGetter cosmeticDataGetter = new CosmeticDataGetter(fileStorage);

        ServiceLocator.Current.Register(userDataGetter, Service.USER_DATA_GETTER);
        ServiceLocator.Current.Register(userStatManager, Service.USER_STAT_MANAGER);
        ServiceLocator.Current.Register(cosmeticDataGetter, Service.COSMETIC_DATA_GETTER);

        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
