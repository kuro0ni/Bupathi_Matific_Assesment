#if (UNITY_EDITOR)
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SaveDataRemover : MonoBehaviour
{

    [Button("Remove Save Data")]
    public void RemoveAllSavedData()
    {
        PlayerPrefs.DeleteAll();

        try
        {
            File.Delete(PathsConfig.USER_DATA_PATH);
            File.Delete(PathsConfig.COSMETICS_PATH);

            Debug.Log("Saved data removed successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("An error occured while deleting saved data.");
            Debug.LogError(ex.Message);
        }
    }
}
#endif