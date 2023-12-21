using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreviewAnimationController : MonoBehaviour
{
    public GameObject Character;
    Vector3 OriginalCharacterPos;
    public GameObject CosmeticSelectionUI;
    public GameObject PlayerStatsUI;
    public GameObject PreviewBtn;
    public GameObject BackBtn;

    public GameObject PreviewBackDropWorld;
    public GameObject RayEffect;

    LTDescr RayAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPreviewBtnClicked()
    {
        OriginalCharacterPos = Character.transform.position;

        //Hide preview btn
        LeanTween.scale(PreviewBtn, Vector3.zero, 0.5f).setEaseOutQuad();

        //Hide UI sections
        LeanTween.scale(CosmeticSelectionUI, Vector3.zero, 0.5f).setEaseOutQuad().setDelay(0.3f);
        LeanTween.scale(PlayerStatsUI, Vector3.zero, 0.5f).setEaseOutQuad().setDelay(0.3f);

        //Show back drop
        LeanTween.scale(PreviewBackDropWorld, Vector3.one * 3.6f, 1.2f).setEaseOutQuad().setDelay(0.4f);

        //Animate character to the center of the screen
        Vector3 lookAtPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        LeanTween.moveLocal(Character, lookAtPosition, 1f).setEaseOutQuad().setDelay(1f).setOnComplete(() =>
        {
            //Show and rotate Ray effect
            LeanTween.scale(RayEffect, Vector3.one * 0.9f, 0.4f).setEaseOutQuad();
            RayAnim = LeanTween.rotateAroundLocal(RayEffect, RayEffect.transform.forward, 360, 7f).setEaseLinear().setRepeat(-1);
        }); ;

        //Show and back btn
        LeanTween.scale(BackBtn, Vector3.one, 0.5f).setEaseOutBounce().setDelay(2f);
    }

    public void OnBackBtnClicked()
    {
        //Hide and back btn
        LeanTween.scale(BackBtn, Vector3.zero, 0.5f).setEaseOutBounce();

        //Hide back drop
        LeanTween.scale(PreviewBackDropWorld, Vector3.zero, 1.2f).setEaseOutQuad().setDelay(0.4f).setOnComplete(() =>
        {
            //Show preview btn
            LeanTween.scale(PreviewBtn, Vector3.one, 0.5f).setEaseOutQuad();

            //Show UI sections
            LeanTween.scale(CosmeticSelectionUI, Vector3.one, 0.5f).setEaseOutQuad();
            LeanTween.scale(PlayerStatsUI, Vector3.one, 0.5f).setEaseOutQuad();
        }); ;

        //Hide and stop ray effect rotation
        LeanTween.scale(RayEffect, Vector3.zero, 0.4f).setEaseOutQuad().setOnComplete(() =>
        {
            LeanTween.cancel(RayAnim.id);
        });

        //Animate character back to the original spot
        LeanTween.moveLocal(Character, OriginalCharacterPos, 1f).setEaseOutQuad().setDelay(0.4f);
    }


}
