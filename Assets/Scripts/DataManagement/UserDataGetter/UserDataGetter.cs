using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class UserDataGetter : IUserDataGetter, IGameService
{
    IGameDataStorage GameDataStorage;

    private UserData Data;

    public UnityEvent<UserData> OnUserStatChanged;
    public UserDataGetter(IGameDataStorage gameDataStorage)
    {
        GameDataStorage = gameDataStorage;

        OnUserStatChanged = new UnityEvent<UserData>();

        try
        {
            Data = gameDataStorage.Download<UserData>();
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError("Could not find User data file in the local drive.");

            Data = new UserData
            {
                Level = Random.Range(0, 8),
                Coins = Random.Range(50, 1500)
            };

            SetData(Data);
        }
    }

    public UserData GetData()
    {
        return Data;
    }

    public void SetData(UserData data)
    {
        Data = data;
        GameDataStorage.Upload(data);
        OnUserStatChanged.Invoke(data);
    }

    public void ListenToOnUserDataChange(UnityAction<UserData> callback)
    {
        Debug.Log("registered to On user data change event");
        OnUserStatChanged.AddListener(callback);
    }
}
