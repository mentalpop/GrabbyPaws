using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    public AnimatedUIContainer container;
    public TextMeshProUGUI cashDisplay;

    private void OnEnable() {
        container.OnEffectComplete += Container_OnEffectComplete;
    }

    private void OnDisable() {
        container.OnEffectComplete -= Container_OnEffectComplete;
    }

    private void Container_OnEffectComplete(bool reverse) {
        if (reverse) {
            gameObject.SetActive(false);
        }
    }

    public void Close() {
        if (!container.gTween.doReverse)
            container.gTween.Reverse();
    }

    public void UpdateCashDisplay() {
        cashDisplay.text = string.Format("{0:n0}", Currency.instance.Cash); //Display currency amount with commas, no decimals (although there shouldn't be any!!)
    }
}