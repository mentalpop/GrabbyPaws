using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class RaccoonProfileImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image myImage;
    public Vector2 tooltipOffset;

    [HideInInspector] public SneakDiary sneakDiaryRef;
    [HideInInspector] public NPCProfileUIData profileData;

    private GameObject tooltip;

    public void Unpack(NPCProfileUIData _profileData, SneakDiary _sneakDiaryRef) {
        sneakDiaryRef = _sneakDiaryRef;
        profileData = _profileData;
        myImage.sprite = profileData.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (tooltip == null) {
            Vector2 positionVector = new Vector2(transform.position.x + ScreenSpace.Convert(tooltipOffset.x), transform.position.y + ScreenSpace.Convert(tooltipOffset.y));
            tooltip = sneakDiaryRef.TooltipOpenSmall(profileData.rName, positionVector, false);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (tooltip != null) {
            Destroy(tooltip);
            tooltip = null;
        }
    }
}