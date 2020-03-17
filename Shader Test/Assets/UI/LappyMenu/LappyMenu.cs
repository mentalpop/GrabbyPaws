using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LappyMenu : MonoBehaviour
{
    public SneakDiary sneakDiary;
    public NotSecrets notSecrets;
    public OptionsMenu optionsMenu;
    public HellaHockster hellaHuckster;
    public WishListWindow wishList;
    public Image lappyBG;
    public ClickToClose clickToClose;
    public List<Sprite> lappyBGs = new List<Sprite>();
    [HideInInspector] public int chosenBGIndex = 0;
    
    public TabSortMenu startTabsSortMenu;
    public List<TabData> tabs = new List<TabData>();
    public ButtonGeneric startButton;

	public ConfirmationPromptData promptQuit;
	public ConfirmationPromptData promptSave;
	private ConfirmationWindow confirmationWindow;
	private bool awaitingConfirmation = false;

	private void OnClickStart() {
		startTabsSortMenu.gameObject.SetActive(!startTabsSortMenu.gameObject.activeInHierarchy);
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
		if (awaitingConfirmation) {
			awaitingConfirmation = false;
			confirmationWindow.OnChoiceMade -= OnConfirm;
		}
    }
    
	private void OnConfirm(bool _choice) {
		awaitingConfirmation = false;
		confirmationWindow.OnChoiceMade -= OnConfirm;
		if (_choice) {
            if (confirmationWindow.promptData.promptID == ConfirmationPromptID.Quit) {
                Application.Quit();
            }
            if (confirmationWindow.promptData.promptID == ConfirmationPromptID.Save) {
                UI.instance.SaveGameData(0);
            }
		} else {
			Debug.Log("User selected NOPE");
		}
	}

    void Start() {
        startTabsSortMenu.InstantiateTabs(tabs);
    }

    public void SelectStartMenuItem(int _activeTab) {
        switch(_activeTab) {
            case 0: //Rewind Time
                hellaHuckster.gameObject.SetActive(true);
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
                optionsMenu.gameObject.SetActive(true);
                break;
            case 5: //Quit to Title
                confirmationWindow = UI.RequestConfirmation(promptQuit);
                confirmationWindow.OnChoiceMade += OnConfirm;
			    awaitingConfirmation = true;
                break;
            case 6: //Save Game
                confirmationWindow = UI.RequestConfirmation(promptSave);
                confirmationWindow.OnChoiceMade += OnConfirm;
			    awaitingConfirmation = true;
                break;
            case 7: //Wish List
                wishList.gameObject.SetActive(true);
                break;
        }
    }

    public void Close() {
        gameObject.SetActive(false); //For now, just close instantly
    }

    public void SetBackground(int _bgIndex) {
        chosenBGIndex = _bgIndex;
        lappyBG.sprite = lappyBGs[_bgIndex];
    }
}