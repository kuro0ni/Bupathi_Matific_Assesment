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

        IGameDataStorage cosmeticDataStorage, userDataStorage;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            IParser jsonParser = new JSONParser();
            cosmeticDataStorage = new PlayerPrefStorage(PathsConfig.COSMETICS_PREF_KEY, PlayerPrefType.STRING, jsonParser);
            userDataStorage = new PlayerPrefStorage(PathsConfig.USER_DATA_PREF_KEY, PlayerPrefType.STRING, jsonParser);
        }
        else
        {
            JSONFileReader cosmeticsJsonFileReader = new JSONFileReader(PathsConfig.COSMETICS_PATH);
            JSONFileReader userDataJsonFileReader = new JSONFileReader(PathsConfig.USER_DATA_PATH);

            cosmeticDataStorage = new FileStorage(cosmeticsJsonFileReader);
            userDataStorage = new FileStorage(userDataJsonFileReader);
        }

        CosmeticDataGetter cosmeticDataGetter = new CosmeticDataGetter(cosmeticDataStorage);
        UserDataGetter userDataGetter = new UserDataGetter(userDataStorage);

        ServiceLocator.Current.Register(userDataGetter, Service.USER_DATA_GETTER);
        ServiceLocator.Current.Register(cosmeticDataGetter, Service.COSMETIC_DATA_GETTER);
    }
}
