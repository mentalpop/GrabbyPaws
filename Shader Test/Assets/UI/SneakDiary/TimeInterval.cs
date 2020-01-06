using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PixelCrushers.DialogueSystem;

public class TimeInterval : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite xSmall;
    public Sprite xLarge;
    public Image xImage;
    public Vector2 tooltipOffset;
    public bool faceLeft = true;
    //public float tooltipLargeWidth = 498f;

    public SneakDiary sneakDiaryRef;

    public TimeIntervalData timeIntervalData;
    private GameObject tooltip;
    private GameObject tooltipLarge;

    private void Start() {
//Debug
        Unpack(timeIntervalData);
    }

    public void Unpack(TimeIntervalData _timeIntervalData) {
        timeIntervalData = _timeIntervalData;
        xImage.sprite = timeIntervalData.isMajorEvent ? xLarge : xSmall; //Set size of X sprite
        xImage.SetNativeSize();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (tooltipLarge == null) {
            Vector2 positionVector = new Vector2(transform.position.x + ScreenSpace.Convert(tooltipOffset.x/* + (faceLeft ? 0f: tooltipLargeWidth)*/), transform.position.y + ScreenSpace.Convert(tooltipOffset.y));
            tooltipLarge = sneakDiaryRef.TooltipOpenLarge(timeIntervalData, positionVector, faceLeft);
            if (tooltip != null) {
                Destroy(tooltip);
                tooltip = null;
            }
        }
        /*
        var quests = QuestLog.GetAllQuests(QuestState.Abandoned | QuestState.Active | QuestState.Failure | QuestState.Success | QuestState.Unassigned, false);
        Debug.Log("quests: "+quests.Length);
        foreach (var quest in quests) {
            Debug.Log("quest: "+quest);
        }
        //*/
        //QuestLog.CompleteQuest(QuestNames.q001TwilightCottonCandy.ToString());
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (tooltipLarge == null && tooltip == null) {
            Vector2 positionVector = new Vector2(transform.position.x + ScreenSpace.Convert(tooltipOffset.x), transform.position.y + ScreenSpace.Convert(tooltipOffset.y));
            tooltip = sneakDiaryRef.TooltipOpenSmall(timeIntervalData.title, positionVector, faceLeft);
        }
            //tooltip = sneakDiaryRef.TooltipOpenSmall(timeIntervalData, new Vector2(transform.position.x + ScreenSpace.Convert(tooltipOffset.x), transform.position.y + ScreenSpace.Convert(tooltipOffset.y)));
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