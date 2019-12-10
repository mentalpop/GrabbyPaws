using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public ReadableData sampleBook;

    void Update() {
//Debug
        if (Input.GetKeyDown(KeyCode.B)) { //B for Book
            UI.instance.DisplayReadable(sampleBook);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) { //Add LOTS of funds
            Currency.instance.Cash += 10000000m;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) { //Subtract Funds
            Currency.instance.Cash -= 100m;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMultiply)) { //Try a Purchase, display if successful
            if (Currency.instance.Purchase(500m)) {
                Debug.Log("Purchase successful!");
            } else {
                Debug.Log("Not enough funds!");
            }
        }
    }
}