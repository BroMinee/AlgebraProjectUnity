using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomusSlotManager : MonoBehaviour
{
    [SerializeField] Slider sliderJumpBoost;
    [SerializeField] Slider sliderFalling;
    [SerializeField] GameObject bonusJumpBoostUI;
    [SerializeField] GameObject bonusSlowFallingUI;
    [SerializeField] GameObject UISword;
    [SerializeField] GameObject UISwordRange;

    private void Awake()
    {
        bonusJumpBoostUI.SetActive(false);
        bonusSlowFallingUI.SetActive(false);
        UISword.SetActive(true);
        UISwordRange.SetActive(false);
    }

    public void SetDoubleJump(float step,int max)
    {
        bonusJumpBoostUI.SetActive(true);
        sliderJumpBoost.maxValue = max;
        sliderJumpBoost.value = max - step;

    }

    public void SetSlowFalling(float step,int max)
    {
        bonusSlowFallingUI.SetActive(true);
        sliderFalling.maxValue = max;
        sliderFalling.value = max - step;
    }

    public void SetSwordLongRange()
    {
        UISwordRange.SetActive(true);
        UISword.SetActive(false);
    }

    public void DisableFallingUI()
    {
        bonusSlowFallingUI.SetActive(false);
    }
    public void DisableJumpUI()
    {
        bonusJumpBoostUI.SetActive(false);
    }
}
