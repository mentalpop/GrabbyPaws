using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HocksterLineItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject arrow;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemQuantity;
    public Color colorNeutral;
    public Color colorMOver;
    public GameObject itemTooltip;

    private InventoryItem iItem;
    private List<InventoryItem> myList;
    private List<InventoryItem> otherList;
    private HellaHockster hellaHockster;
    private bool mouseOver = false;

    public void Unpack(InventoryItem _item, List<InventoryItem> _myList, List<InventoryItem> _otherList, HellaHockster _hellaHockster) {
        iItem = _item;
        myList = _myList;
        otherList = _otherList;
        hellaHockster = _hellaHockster;
        itemName.text = iItem.item.name;
        itemQuantity.text = iItem.quantity.ToString();
    }

    public void OnPointerEnter(PointerEventData evd) {
        mouseOver = true;
        arrow.SetActive(true);
        itemName.color = colorMOver;
        itemQuantity.color = colorMOver;
    }

	public void OnPointerExit (PointerEventData evd) {
        mouseOver = false;
        arrow.SetActive(false);
        itemName.color = colorNeutral;
        itemQuantity.color = colorNeutral;
    }
		
	public void OnPointerClick (PointerEventData evd) {
        if (evd.button == PointerEventData.InputButton.Right) {
    //Move one item at a time
            hellaHockster.MoveItem(myList, otherList, iItem.item, 1);
        } else {
    //Move entiire stack
            hellaHockster.MoveItem(myList, otherList, iItem.item, iItem.quantity);
        }
	}
}