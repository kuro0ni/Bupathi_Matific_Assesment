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

    public GameObject PreviewBackDropUI;
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

        LeanTween.scale(PreviewBtn, Vector3.zero, 0.5f).setEaseOutQuad();

        LeanTween.scale(CosmeticSelectionUI, Vector3.zero, 0.5f).setEaseOutQuad().setDelay(0.3f);
        LeanTween.scale(PlayerStatsUI, Vector3.zero, 0.5f).setEaseOutQuad().setDelay(0.3f);

        LeanTween.scale(PreviewBackDropWorld, Vector3.one * 3.6f, 1.2f).setEaseOutQuad().setDelay(0.4f);

        Vector3 lookAtPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

        LeanTween.moveLocal(Character, lookAtPosition, 1f).setEaseOutQuad().setDelay(1f).setOnComplete(() =>
        {
            LeanTween.scale(RayEffect, Vector3.one * 0.9f, 0.4f).setEaseOutQuad();
            RayAnim = LeanTween.rotateAroundLocal(RayEffect, RayEffect.transform.forward, 360, 7f).setEaseLinear().setRepeat(-1);
        }); ;

        LeanTween.scale(BackBtn, Vector3.one, 0.5f).setEaseOutBounce().setDelay(2f);
    }

    public void OnBackBtnClicked()
    {
        LeanTween.scale(BackBtn, Vector3.zero, 0.5f).setEaseOutBounce();

        LeanTween.scale(PreviewBackDropWorld, Vector3.zero, 1.2f).setEaseOutQuad().setDelay(0.4f).setOnComplete(() =>
        {
            LeanTween.scale(PreviewBtn, Vector3.one, 0.5f).setEaseOutQuad();

            LeanTween.scale(CosmeticSelectionUI, Vector3.one, 0.5f).setEaseOutQuad();
            LeanTween.scale(PlayerStatsUI, Vector3.one, 0.5f).setEaseOutQuad();
        }); ;

        LeanTween.scale(RayEffect, Vector3.zero, 0.4f).setEaseOutQuad().setOnComplete(() =>
        {
            LeanTween.cancel(RayAnim.id);
        });

        LeanTween.moveLocal(Character, OriginalCharacterPos, 1f).setEaseOutQuad().setDelay(0.4f);
    }


}
