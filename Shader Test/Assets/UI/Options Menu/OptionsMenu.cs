using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public LappyMenu lappyMenu;
	public ButtonGeneric closeButton;
    public ClickToClose clickToClose;

	public DropDownMenu screenMode;
	public DropDownMenu resolution;
	public DropDownMenu quality;
	public DropDownMenu lappyBG;

	private void OnEnable() {
        clickToClose.OnClick += Close;
		closeButton.OnClick += Close;
	//Set Screen Mode drop-down
		if (Screen.fullScreen) {
		//Fullscreen
			screenMode.chosenIndex = 1;
		} else {
		//Windowed
			screenMode.chosenIndex = 0;
		}
		screenMode.SetHeader(screenMode.chosenIndex);
		screenMode.OnChoiceMade += ScreenMode_OnChoiceMade;
	//Screen Resolution
		switch (Screen.width) {
			case 1920: resolution.chosenIndex = 0; break;
			case 1600: resolution.chosenIndex = 1; break;
			case 1366: case 1360: resolution.chosenIndex = 2; break;
			case 1280: resolution.chosenIndex = 3; break;
			default: resolution.chosenIndex = 4; break;
		}
		resolution.SetHeader(resolution.chosenIndex);
		resolution.OnChoiceMade += Resolution_OnChoiceMade;
	//Quality
		quality.OnChoiceMade += Quality_OnChoiceMade;
	//Lappy BG
		lappyBG.chosenIndex = lappyMenu.chosenBGIndex;
		lappyBG.SetHeader(lappyBG.chosenIndex);
		lappyBG.OnChoiceMade += LappyBG_OnChoiceMade;
	}

	private void LappyBG_OnChoiceMade(int choiceMade) {
		lappyMenu.SetBackground(choiceMade);
	}

	private void Quality_OnChoiceMade(int choiceMade) {
		Debug.Log("choiceMade: "+choiceMade);
	}

	private void Resolution_OnChoiceMade(int choiceMade) {
		switch (choiceMade) {
			case 0: Screen.SetResolution(1920, 1080, Screen.fullScreen); break;
			case 1: Screen.SetResolution(1600, 900, Screen.fullScreen); break;
			case 2: Screen.SetResolution(1366, 768, Screen.fullScreen); break;
			case 3: Screen.SetResolution(1280, 720, Screen.fullScreen); break;
			case 4: Screen.SetResolution(1176, 664, Screen.fullScreen); break;
		}
	}

	private void ScreenMode_OnChoiceMade(int choiceMade) {
		switch (choiceMade) {
			case 0: Screen.fullScreen = false; break;
			case 1: Screen.fullScreen = true; break;
		}
	}

	private void OnDisable() {
        clickToClose.OnClick -= Close;
		closeButton.OnClick -= Close;
		screenMode.OnChoiceMade -= ScreenMode_OnChoiceMade;
		resolution.OnChoiceMade -= Resolution_OnChoiceMade;
		quality.OnChoiceMade -= Quality_OnChoiceMade;
		lappyBG.OnChoiceMade -= LappyBG_OnChoiceMade;
	}

	private void Close() {
		gameObject.SetActive(false);
	}
}