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

    void Start() {
        RectResize(countChildTransform.childCount);
    }

    public void RectResize(int childCount) {
        int numToResize = Mathf.Min(maxChildrenOnScreen, childCount);
        myRect.sizeDelta = new Vector2(myRect.rect.width, heightPerChild * numToResize);
    }
}