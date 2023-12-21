using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabBodyContainer : MonoBehaviour
{
    void Start()
    {
        PlayInAnimation();
    }

    private void PlayInAnimation()
    {
        transform.localScale = new Vector3(0,1,1);
        LeanTween.scaleX(gameObject, 1, 1f).setEaseOutBounce();
    }
}
