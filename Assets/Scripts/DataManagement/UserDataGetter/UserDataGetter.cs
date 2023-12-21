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
        catch (System.Exception ex)
        {
            Debug.LogWarning("Could not find stored user data");
            Debug.LogWarning(ex.Message);         
        }
    }

    public UserData GetData()
    {
        if (Data == null)
        {
            CreateRandomUserData();
        }

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
        OnUserStatChanged.AddListener(callback);
    }

    private void CreateRandomUserData()
    {
        Data = new UserData
        {
            Level = Random.Range(0, 8),
            Coins = Random.Range(50, 1500)
        };

        SetData(Data);
    }
}
