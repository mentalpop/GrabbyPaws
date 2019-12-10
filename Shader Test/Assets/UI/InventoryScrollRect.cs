using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScrollRect : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform contentTransform;
    public ScrollResize scrollResize;
    public BottomCapAdjust bottomCapAdjust;

    private bool myBool;
    private List<GameObject> slots = new List<GameObject>();

    public void Unpack(List<Item> items) {
    //Clear the slots first
        if (slots.Count > 0) {
            foreach (var slot in slots) {
                Destroy(slot);
            }
            slots.Clear();
        }
        foreach (var item in items) {
    //Instantiate each item
            GameObject gameObject = Instantiate(slotPrefab, contentTransform, false);
            slots.Add(gameObject);
            InventorySlot slot = gameObject.GetComponent<InventorySlot>();
            slot.Unpack(item);
        }
        scrollResize.RectResize(slots.Count);
        bottomCapAdjust.UpdateHeight(scrollResize.myRect.rect.height);
    }

    /*
    void Update() {

    }
    //*/
}