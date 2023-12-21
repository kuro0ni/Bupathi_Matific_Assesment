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

        ServiceLocator.Current.Register(userDataGetter, Service.USER_DATA_GETTER);
        ServiceLocator.Current.Register(cosmeticDataGetter, Service.COSMETIC_DATA_GETTER);
    }
}
