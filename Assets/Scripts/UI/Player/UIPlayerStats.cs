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
        IUserStatManager userStatManager = (IUserStatManager)ServiceLocator.Current.Get(Service.USER_STAT_MANAGER);

        userStatManager.ListenToOnUserDataChange(OnUserDataChange);

        UserData userData = userStatManager.GetUserData();

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
}
