using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class UI : Singleton<UI>
{
    public Sonos sonosAudio;
    public GameObject HUD;
    public GameObject InventoryDisplay;
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

    /*
    void Start() {

    }
    //*/
    private void ShowHideCurrencyDisplay() {
        currencyDisplay.gameObject.SetActive(doShowCurrencyDisplay || InventoryDisplay.activeSelf);
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
    //Debug
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
            Currency.instance.Cash += 10000000m;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            Currency.instance.Cash -= 100m;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMultiply)) {
            if (Currency.instance.Purchase(500m)) {
                Debug.Log("Purchase successful!");
            } else {
                Debug.Log("Not enough funds!");
            }
        }

        if (Input.GetButtonDown("Inventory")) {
            ShowInventoryDisplay();
        }
    }
}