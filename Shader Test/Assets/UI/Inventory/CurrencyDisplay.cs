using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    public TextMeshProUGUI cashDisplay;

    public void UpdateCashDisplay() {
        cashDisplay.text = string.Format("{0:n0}", Currency.instance.Cash); //Display currency amount with commas, no decimals (although there shouldn't be any!!)
    }
}