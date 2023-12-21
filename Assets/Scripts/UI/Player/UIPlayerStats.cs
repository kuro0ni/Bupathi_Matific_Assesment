using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    public TMP_Text PlayerLevelText;
    public TMP_Text PlayerCoinsText;

    void Start()
    {
        PlayInAnimation();

        UserData userData = GetUserData();
        PopulateStats(userData);
    }

    /// <summary>
    /// Get user data from the Getter and register for the OnUserDataChange event so on an event where UserData is modified it will be notified to this object
    /// </summary>
    /// <returns></returns>
    private UserData GetUserData()
    {
        IUserDataGetter userDataGetter = ServiceLocator.Current.Get<IUserDataGetter>(Service.USER_DATA_GETTER);

        userDataGetter.ListenToOnUserDataChange(OnUserDataChange);

        UserData userData = userDataGetter.GetData();

        return userData;
    }

    private void PopulateStats(UserData userData)
    {       
        PlayerLevelText.text = $"Lvl.{userData.Level}";
        PlayerCoinsText.text = userData.Coins.ToString();
    }

    private void OnUserDataChange(UserData data)
    {
        PopulateStats(data);
    }

    private void PlayInAnimation()
    {
        transform.localScale = new Vector3(1,0,1);
        LeanTween.scaleY(gameObject, 1, 1f).setEaseOutBounce();
    }
}
