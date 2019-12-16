using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class UI : Singleton<UI>
{
    public Sonos sonosAudio;
    public GameObject HUD;
    public GameObject InventoryDisplay;
    public LappyMenu lappy;
    [Header("Readables")]
    public Readable book;
    [Header("Currency")]
    public float timeShowCurrency = 3f;
    public Currency currency;
    public CurrencyDisplay currencyDisplay;

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