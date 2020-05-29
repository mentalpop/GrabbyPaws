using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedUIContainer : MonoBehaviour
{
    public RectTransform myRect;

    public delegate void EffectComplete(bool reverse);
    public event EffectComplete OnEffectComplete = delegate { };

    [HideInInspector] public GTween gTween;

    private void Awake() {
        gTween = new GTween(0.3f, 0f, 1f, false);
        myRect.anchoredPosition = new Vector2(myRect.anchoredPosition.x, -myRect.sizeDelta.y);
    }

    private void OnEnable() {
        gTween.Reset();
    }
    private void OnDisable() {
        myRect.anchoredPosition = new Vector2(myRect.anchoredPosition.x, -myRect.sizeDelta.y);
    }

    private void Update() {
        if (gTween.effectActive) {
            float tweenVal = gTween.DoTween();
            if (gTween.effectActive) {
                myRect.anchoredPosition = new Vector2(myRect.anchoredPosition.x, -myRect.sizeDelta.y + myRect.sizeDelta.y * tweenVal);
            } else {
                if (gTween.doReverse) {

                } else {
                    myRect.anchoredPosition = new Vector2(myRect.anchoredPosition.x, 0f);
                }
                OnEffectComplete.Invoke(gTween.doReverse);
            }
        }
    }
}