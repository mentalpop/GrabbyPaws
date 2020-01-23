using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScrollRect : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform contentTransform;
    public ScrollResize scrollResize;
    public InventoryDisplay inventoryDisplay;
    public BottomCapAdjust bottomCapAdjust;

    private List<GameObject> slots = new List<GameObject>();

    public void Unpack(List<InventoryItem> items) {
    //Clear the slots first
        if (slots.Count > 0) {
            foreach (var slot in slots) {
                Destroy(slot);
            }
            slots.Clear();
        }
        foreach (var iItem in items) {
    //Instantiate each item
            if (iItem.item.category == inventoryDisplay.inventoryDisplayType) {
                GameObject gameObject = Instantiate(slotPrefab, contentTransform, false);
                slots.Add(gameObject);
                InventorySlot slot = gameObject.GetComponent<InventorySlot>();
                slot.Unpack(iItem);
            }
        }
        scrollResize.RectResize(slots.Count);
        bottomCapAdjust.UpdateHeight(scrollResize.myRect.rect.height);
    }
}