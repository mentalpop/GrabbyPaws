using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LappyMenu : MonoBehaviour
{
    public SneakDiary sneakDiary;
    public NotSecrets notSecrets;
    //public Inventory inventory;
    public ClickToClose clickToClose;
    
    public TabSortMenu startTabsSortMenu;
    public List<TabData> tabs = new List<TabData>();
    public ButtonGeneric startButton;

	private void OnClickStart() {
		startTabsSortMenu.gameObject.SetActive(true);
	}

    private void OnEnable() {
        startTabsSortMenu.OnTabSelected += SelectStartMenuItem;
        clickToClose.OnClick += Close;
        startButton.OnClick += OnClickStart;
    }

    private void OnDisable() {
        startTabsSortMenu.OnTabSelected -= SelectStartMenuItem;
        clickToClose.OnClick -= Close;
		startButton.OnClick -= OnClickStart;
    }

    void Start() {
        startTabsSortMenu.InstantiateTabs(tabs);
    }

    public void SelectStartMenuItem(int _activeTab) {
        startTabsSortMenu.gameObject.SetActive(false);
        switch(_activeTab) {
            case 0: //Rewind Time

                break;
            case 1: //Sneak Diary
                sneakDiary.gameObject.SetActive(true);
                break;
            case 2: //Not Secrets
                notSecrets.gameObject.SetActive(true);
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