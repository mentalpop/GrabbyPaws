using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LappyMenu : MonoBehaviour
{
    //public Inventory inventory;
    public ClickToClose clickToClose;
    
    public TabSortMenu startTabsSortMenu;
    public List<TabData> tabs = new List<TabData>();

    private void OnEnable() {
        startTabsSortMenu.OnTabSelected += SelectStartMenuItem;
        clickToClose.OnClick += Close;
    }

    private void OnDisable() {
        startTabsSortMenu.OnTabSelected -= SelectStartMenuItem;
        clickToClose.OnClick -= Close;
    }

    void Start() {
        startTabsSortMenu.InstantiateTabs(tabs);
    }

    public void SelectStartMenuItem(int _activeTab) {
        //UpdateDisplay();
    }

    public void Close() {
        gameObject.SetActive(false); //For now, just close instantly
    }
    /*
    
    public void UpdateDisplay() {

    }
    //*/
}