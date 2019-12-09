using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public List<Item> testList = new List<Item>();
    public TabSortMenu inventoryTabMenu;
    public InventoryScrollRect inventoryScrollRect;
    public List<TabData> tabs = new List<TabData>();

    private List<TabSortItem> tabSortItems = new List<TabSortItem>();

    private bool myBool;

    private void OnEnable() {
        inventoryTabMenu.OnTabSelected += SetActiveTab;
    }

    private void OnDisable() {
        inventoryTabMenu.OnTabSelected -= SetActiveTab;
    }

    void Start() {
        inventoryTabMenu.InstantiateTabs(tabs);
    }
    public void SetActiveTab(int _activeTab) {
        inventoryScrollRect.Unpack(testList);
    //Set position in Heirarchy to be one more than the active tab
        inventoryScrollRect.transform.SetAsLastSibling(); //Do this first to put it to the end of the list
        inventoryScrollRect.transform.SetSiblingIndex(inventoryTabMenu.tabs[_activeTab].transform.GetSiblingIndex() + 1);
        /*
        foreach (var scrollRect in inventoryScrollRect) {
            scrollRect.gameObject.SetActive(inventoryScrollRect.IndexOf(scrollRect) == _activeTab);
            
        }
        //*/
    }
}