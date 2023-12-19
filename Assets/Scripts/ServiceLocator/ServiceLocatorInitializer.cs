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

        JSONFileReader cosmeticsJsonFileReader = new JSONFileReader(PathsConfig.COSMETICS_PATH);
        JSONFileReader userDataJsonFileReader = new JSONFileReader(PathsConfig.USER_DATA_PATH);

        FileStorage cosmeticsFileStorage = new FileStorage(cosmeticsJsonFileReader);
        FileStorage userDatafileStorage = new FileStorage(userDataJsonFileReader);

        CosmeticDataGetter cosmeticDataGetter = new CosmeticDataGetter(cosmeticsFileStorage);
        UserDataGetter userDataGetter = new UserDataGetter(userDatafileStorage);

        //UserStatManager userStatManager = new UserStatManager(userDataGetter);


        ServiceLocator.Current.Register(userDataGetter, Service.USER_DATA_GETTER);
        //ServiceLocator.Current.Register(userStatManager, Service.USER_STAT_MANAGER);
        ServiceLocator.Current.Register(cosmeticDataGetter, Service.COSMETIC_DATA_GETTER);

        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
