using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public Inventory inventory;
    
    public TabSortMenu inventoryTabMenu;
    public InventoryScrollRect inventoryScrollRect;
    public List<TabData> tabs = new List<TabData>();

    [HideInInspector] public CategoryItem inventoryDisplayType;

    //private List<TabSortItem> tabSortItems = new List<TabSortItem>();

    private bool myBool;

    private void OnEnable() {
        inventoryTabMenu.OnTabSelected += SetActiveTab;
        inventory.OnItemChanged += UpdateDisplay;
    }

    private void OnDisable() {
        inventoryTabMenu.OnTabSelected -= SetActiveTab;
        inventory.OnItemChanged -= UpdateDisplay;
    }

    void Start() {
        inventoryTabMenu.InstantiateTabs(tabs);
    }
    public void SetActiveTab(int _activeTab) {
        inventoryDisplayType = (CategoryItem)_activeTab;
        Debug.Log("inventoryDisplayType" + ": " + inventoryDisplayType);
        UpdateDisplay();
    //Set position in Heirarchy to be one more than the active tab
        inventoryScrollRect.transform.SetAsLastSibling(); //Do this first to put it to the end of the list
        inventoryScrollRect.transform.SetSiblingIndex(inventoryTabMenu.tabs[_activeTab].transform.GetSiblingIndex() + 1);
        /*
        foreach (var scrollRect in inventoryScrollRect) {
            scrollRect.gameObject.SetActive(inventoryScrollRect.IndexOf(scrollRect) == _activeTab);
            
        }
        //*/
    }

    public void UpdateDisplay() {
        inventoryScrollRect.Unpack(inventory.items);
    }
}