using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;

public class RaccoonProfileImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string rName = "RACCOON NAME SOMETIMES LONG";
    public Vector2 tooltipOffset;
    public SneakDiary sneakDiaryRef;

    private GameObject tooltip;

    public void OnPointerEnter(PointerEventData eventData) {
        if (tooltip == null) {
            Vector2 positionVector = new Vector2(transform.position.x + ScreenSpace.Convert(tooltipOffset.x), transform.position.y + ScreenSpace.Convert(tooltipOffset.y));
            tooltip = sneakDiaryRef.TooltipOpenSmall(rName, positionVector, false);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (tooltip != null) {
            Destroy(tooltip);
            tooltip = null;
        }
    }
}