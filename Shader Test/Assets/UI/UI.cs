using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public enum NightPhases
{
    p1Twilight,
    p2MoonRise,
    p3Midnight,
    p4MoonFall,
    p5Dawn
}

public enum QuestNames {
    q001TwilightCottonCandy,
    q001TwilightCottonCandyEndFlag
    /*,
    q002TwilightCCandy,
    q010TwilightCCandy,
    q004Twilight,
    //*/
}

public enum NPCLocations {
    /*
    npc001Twilight01Riven,
    npc001Twilight02Riven,
    npc001Twilight03Riven,
    npc001Twilight04Riven,
    npc001Twilight05Riven,
    npc002,
    q010TwilightCCandy,
    q004Twilight,
    //*/
}

public class UI : Singleton<UI>
{
    public static string saveFileName = "grabby.paws";

    public Sonos sonosAudio;
    public GameObject HUD;
    public LappyMenu lappy;
    //public FlagRepository flagRepository;
    [Header("Readables")]
    public Readable book;
    [Header("Currency")]
    public float timeShowCurrency = 3f;
    public Currency currency;
    public CurrencyDisplay currencyDisplay;
    //[Header("Settings")]
    [Header("Inventory")]
    public GameObject InventoryDisplay;
    public Inventory inventory;

    public delegate void FileIOEvent(int fileNum);
    public event FileIOEvent OnSave = delegate { };
    public event FileIOEvent OnLoad = delegate { };

    private bool doShowCurrencyDisplay = false;
    private Coroutine currencyDisplayRoutine;

    private void OnEnable() {
        RegisterSingleton (this);
        currency.OnCashChanged += OnCurrencyChanged;
    }

    private void OnDisable() {
        currency.OnCashChanged -= OnCurrencyChanged;
    }

    public void DisplayReadable(ReadableData rData) {
        if (rData.isBook) {
            book.gameObject.SetActive(true);
            book.Unpack(rData);
        } else {
            Debug.Log("Readable PC not yet implemented!");
        }
    }

    private void ShowHideCurrencyDisplay() {
        currencyDisplay.gameObject.SetActive(doShowCurrencyDisplay || InventoryDisplay.activeSelf);
    }
    private void ShowLappyMenu() {
    //Show / Hide the HUD
        lappy.gameObject.SetActive(!lappy.gameObject.activeSelf);
    }

    private void ShowInventoryDisplay() {
    //Show / Hide the HUD
        InventoryDisplay.SetActive(!InventoryDisplay.activeSelf);
        ShowHideCurrencyDisplay();
    }

    private void OnCurrencyChanged() {
        doShowCurrencyDisplay = true;
        currencyDisplay.UpdateCashDisplay();
        ShowHideCurrencyDisplay();
        if (currencyDisplayRoutine != null)
            StopCoroutine(currencyDisplayRoutine); //Stop before starting another
        currencyDisplayRoutine = StartCoroutine(DelayHideCurrencyDisplay());
    }

    IEnumerator DelayHideCurrencyDisplay() {
        yield return new WaitForSeconds(timeShowCurrency);
        doShowCurrencyDisplay = false;
        ShowHideCurrencyDisplay();
        currencyDisplayRoutine = null;
    }

    void Update() {
//Open / Close menus
        if (Input.GetButtonDown("Inventory")) {
            ShowInventoryDisplay();
        }
        if (Input.GetButtonDown("Kwit")) {
            ShowLappyMenu();
        }
    }
//Save / Load
    public void SaveGameData(int fileNum) {
        Debug.Log("Game Saved");
        OnSave?.Invoke(fileNum);
    }

    public void LoadGameData(int fileNum) {
        Debug.Log("Game Loaded");
        OnLoad?.Invoke(fileNum);
    }
}

public static class ScreenSpace
{
    public static float Width = 1920f;
    public static float Height = 1080f;
	public static float Convert(float variable) {
		return variable * Screen.height / Height;
	}

    public static float Inverse(float variable) {
		return variable * (Height / Screen.height);
	}
}