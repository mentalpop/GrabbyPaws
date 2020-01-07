using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TimeInterval : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite xSmall;
    public Sprite xLarge;
    public Image xImage;
    public Vector2 tooltipOffset;

    private bool faceLeft = true;
    private SneakDiary sneakDiaryRef;
    private TimeIntervalData timeIntervalData;
    private GameObject tooltip;
    private GameObject tooltipLarge;

    public void Unpack(TimeIntervalData _timeIntervalData, SneakDiary _sneakDiaryRef, bool _faceLeft) {
        faceLeft = _faceLeft;
        sneakDiaryRef = _sneakDiaryRef;
        timeIntervalData = _timeIntervalData;
        xImage.sprite = timeIntervalData.isMajorEvent ? xLarge : xSmall; //Set size of X sprite
        xImage.SetNativeSize();
    }

    private void Awake() {
//Convert offset to screen space
        tooltipOffset = new Vector2(ScreenSpace.Convert(tooltipOffset.x), ScreenSpace.Convert(tooltipOffset.y));
    }

    private void Update() {
//Match position of Tooltips
        if (tooltip != null) {
            tooltip.transform.position = new Vector2(transform.position.x + tooltipOffset.x, transform.position.y + tooltipOffset.y);
        }
        if (tooltipLarge != null) {
            tooltipLarge.transform.position = new Vector2(transform.position.x + tooltipOffset.x, transform.position.y + tooltipOffset.y);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (tooltipLarge == null) {
            Vector2 positionVector = new Vector2(transform.position.x + tooltipOffset.x, transform.position.y + tooltipOffset.y);
            tooltipLarge = sneakDiaryRef.TooltipOpenLarge(timeIntervalData, positionVector, faceLeft);
            if (tooltip != null) {
                Destroy(tooltip);
                tooltip = null;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (tooltipLarge == null && tooltip == null) {
            Vector2 positionVector = new Vector2(transform.position.x + tooltipOffset.x, transform.position.y + tooltipOffset.y);
            tooltip = sneakDiaryRef.TooltipOpenSmall(timeIntervalData.title, positionVector, faceLeft);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (tooltipLarge != null) {
            Destroy(tooltipLarge);
            tooltipLarge = null;
        }
        if (tooltip != null) {
            Destroy(tooltip);
            tooltip = null;
        }
    }
}