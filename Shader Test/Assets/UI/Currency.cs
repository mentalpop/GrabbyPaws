﻿using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Singleton<Currency>
{
    public decimal startingFunds = 500m;
    public decimal maxFunds = 99999999m;
    
    public decimal Cash {
        get {return _cash;}
        set {
            _cash = decimal.Round(value);
            if (_cash > maxFunds)
                _cash = maxFunds;
            OnCashChanged?.Invoke();
        }
    }
    private decimal _cash;
    
    public delegate void CurrencyEvent();
    public CurrencyEvent OnCashChanged;
    
    private void OnEnable() {
        RegisterSingleton (this);
        UI.instance.OnSave += Save;
        UI.instance.OnLoad += Load;
    }

    private void OnDisable() {
        UI.instance.OnSave -= Save;
        UI.instance.OnLoad -= Load;
    }

    private string saveString = "currency";

    public void Save(int fileIndex) {
        ES3.Save<decimal>(saveString, _cash);
    }

    public void Load(int fileIndex) {
        Cash = ES3.Load(saveString, startingFunds);
    }

    private void Start() {
        Cash = startingFunds;
    }

    public bool Purchase(float cost) {
//Pass in a float, convert it to decimal
        return Purchase((decimal)cost);
    }

    public bool Purchase(decimal cost) {
        bool purchaseSuccess = false;
        if (Cash >= cost) {
            Cash -= cost;
            purchaseSuccess = true;
        }
        return purchaseSuccess;
    }
}