using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserStatManager : IUserStatManager, IGameService
{
    private UserData UserData;

    public UnityEvent<UserData> OnUserStatChanged;
    public UserStatManager(IUserDataGetter userDataGetter)
    {
        UserData = userDataGetter.GetData();
        OnUserStatChanged = new UnityEvent<UserData>();
    }

    public UserData GetUserData()
    {
        return UserData;
    }

    public void SetUserData(UserData data)
    {
        UserData = data;
        OnUserStatChanged.Invoke(UserData);
    }

    public void ListenToOnUserDataChange(UnityAction<UserData> callback)
    {
        OnUserStatChanged.AddListener(callback);
    }
}
