using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class UI : Singleton<UI>
{
    public Sonos sonosAudio;
    public GameObject HUD;
    public GameObject Inventory;

    private void OnEnable() {
        RegisterSingleton (this);
    }

    /*
    void Start() {

    }
    //*/

    void Update() {
        if (Input.GetButtonDown("Inventory")) {
    //Show / Hide the HUD
            Inventory.SetActive(!Inventory.activeSelf);
        }
    }
}