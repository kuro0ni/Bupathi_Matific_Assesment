using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    public TMP_Text PlayerLevelText;
    public TMP_Text PlayerCoinsText;
    // Start is called before the first frame update
    void Start()
    {
        UserData userData = GetUserData();
        PopulateStats(userData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        Debug.Log("On user data change event raised");
        Debug.Log(data.Coins);
        PopulateStats(data);
    }
}
