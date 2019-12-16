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
        switch(_activeTab) {
            case 0: //Rewind Time

                break;
            case 1: //Sneak Diary

                break;
            case 2: //Not Secrets

                break;
            case 3: //Chat

                break;
            case 4: //Options

                break;
            case 5: //Quit to Title

                break;
            case 6: //Save Game
                UI.instance.SaveGameData(0);
                break;
        }
    }

    public void Close() {
        gameObject.SetActive(false); //For now, just close instantly
    }
    /*
    
    public void UpdateDisplay() {

    }
    //*/
}