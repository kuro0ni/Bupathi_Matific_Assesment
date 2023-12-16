using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    [SerializeField]
    private ICosmeticComponent [] CosmeticComponentList;

    // Start is called before the first frame update
    void Start()
    {
        CosmeticComponentList = GetComponentsInChildren<ICosmeticComponent>();
    }
    
    public void ApplyCosmetic(int cosmeticTypeId, int itemId)
    {
        foreach (ICosmeticComponent component in CosmeticComponentList)
        {
            if (component.GetCosmeticType().GetTypeId() == cosmeticTypeId)
            {
                component.RenderItem(itemId);
            }
        }
    }
}
