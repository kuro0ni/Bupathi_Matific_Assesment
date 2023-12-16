using UnityEngine;

public class UserDataGetter : IUserDataGetter, IGameService
{
    public UserData GetData()
    {
        return new UserData
        {
            Level = Random.Range(0, 8),
            Coins = Random.Range(50, 1500)
        };
    }
}
