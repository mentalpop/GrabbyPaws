using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HocksterScrollRect : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform contentTransform;

    private List<GameObject> lineItems = new List<GameObject>();

    public void Unpack(List<InventoryItem> items) {
    //Clear the slots first
        if (lineItems.Count > 0) {
            foreach (var slot in lineItems) {
                Destroy(slot);
            }
            lineItems.Clear();
        }
        foreach (var iItem in items) {
    //Instantiate each item
            if (iItem.item.category == CategoryItem.Trash) {
                GameObject newGO = Instantiate(itemPrefab, contentTransform, false);
                lineItems.Add(newGO);
                newGO.GetComponent<HocksterLineItem>().Unpack(iItem);
            }
        }
    }
}