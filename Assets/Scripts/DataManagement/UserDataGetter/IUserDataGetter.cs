using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IUserDataGetter
{
    UserData GetData();
    void SetData(UserData data);

    void ListenToOnUserDataChange(UnityAction<UserData> callback);
}
