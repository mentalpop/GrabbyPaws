using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollResize : MonoBehaviour
{
    public RectTransform myRect;
    public ScrollRect scrollRect;
    public Transform countChildTransform;
    public int maxChildrenOnScreen = 9;
    public float heightPerChild = 128;

    private GTween gTween;
    private float targetHeight = 0f;

    private void Awake() {
        gTween = new GTween(0.3f, 0.5f, 1f, false);
    }

    /*
    void Start() {
        RectResize(countChildTransform.childCount);
    }
    //*/

    private void Update() {
        if (gTween.effectActive) {
            float tweenVal = gTween.DoTween();
            if (gTween.effectActive) {
                myRect.sizeDelta = new Vector2(myRect.rect.width, targetHeight * tweenVal);
            } else {
                myRect.sizeDelta = new Vector2(myRect.rect.width, targetHeight);
            }
        }
    }

    public void RectResize(int childCount) {
        int numToResize = Mathf.Min(maxChildrenOnScreen, childCount);
        targetHeight = heightPerChild * numToResize;
        gTween.Reset();
    }
}