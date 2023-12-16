using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStatManager : IUserStatManager, IGameService
{
    private UserData UserData;
    public UserStatManager(IUserDataGetter userDataGetter)
    {
        UserData = userDataGetter.GetData();
    }

    public UserData GetUserData()
    {
        return UserData;
    }
}
