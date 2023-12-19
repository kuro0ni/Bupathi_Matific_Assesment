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
        PopulateStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateStats()
    {
        IUserStatManager userStatManager = (IUserStatManager)ServiceLocator.Current.Get(Service.USER_STAT_MANAGER);
        UserData userData = userStatManager.GetUserData();

        PlayerLevelText.text = $"Lvl.{userData.Level}";
        PlayerCoinsText.text = userData.Coins.ToString();
    }
}
