using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IUserStatManager
{
    UserData GetUserData();
    void SetUserData(UserData data);
    void ListenToOnUserDataChange(UnityAction<UserData> callback);
}
