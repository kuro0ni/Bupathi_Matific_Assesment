using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabBodyContainer : MonoBehaviour
{
    public List<UITabBody> TabBodies;
    // Start is called before the first frame update
    void Start()
    {
        PlayInAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayInAnimation()
    {
        transform.localScale = new Vector3(0,1,1);
        LeanTween.scaleX(gameObject, 1, 1f).setEaseOutBounce();
    }
}
