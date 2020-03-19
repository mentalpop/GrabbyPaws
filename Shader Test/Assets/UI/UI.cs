﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Cinemachine;

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

public enum Secrets {
    s001Test,
    s002Test
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

[DefaultExecutionOrder(-100)]
public class UI : MonoBehaviour
{
    public static string saveFileName = "grabby.paws";

    public Sonos sonosAudio;
    public GameObject HUD;
    public LappyMenu lappy;
    public ConfirmationWindow confirmationWindow;
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
    public CinemachineBrain cBrain;

    public static UI Instance { get; private set; }

    public delegate void FileIOEvent(int fileNum);
    public event FileIOEvent OnSave = delegate { };
    public event FileIOEvent OnLoad = delegate { };

    private bool doShowCurrencyDisplay = false;
    private Coroutine currencyDisplayRoutine;

    private List<GameObject> mouseCursorUsers = new List<GameObject>();

    private void OnEnable() {
        currency.OnCashChanged += OnCurrencyChanged;
    }

    private void OnDisable() {
        currency.OnCashChanged -= OnCurrencyChanged;
    }

    private void Awake() {
    //Singleton Pattern
        if (Instance != null && Instance != this) { 
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
        bool menuIsActive = !lappy.gameObject.activeSelf;
        lappy.gameObject.SetActive(menuIsActive);
        SetMouseState(menuIsActive, lappy.gameObject);
    }

    private void ShowInventoryDisplay() {
    //Show / Hide the HUD
        bool menuIsActive = !InventoryDisplay.activeSelf;
        InventoryDisplay.SetActive(menuIsActive);
        SetMouseState(menuIsActive, InventoryDisplay);
        ShowHideCurrencyDisplay();
    }

    public static void SetMouseState(bool lockMouse, GameObject gameObject) {
        if (lockMouse) {
            Instance.mouseCursorUsers.Add(gameObject);
        } else {
            Instance.mouseCursorUsers.Remove(gameObject);
        }
        bool suppressCamera = false;
        if (Instance.mouseCursorUsers.Count > 0) {
            suppressCamera = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Instance.cBrain != null) {
            CinemachineFreeLook currentCamera = Instance.cBrain.ActiveVirtualCamera as CinemachineFreeLook;
            Debug.Log("currentCamera: "+currentCamera);
            if (currentCamera != null) {
                if (suppressCamera) {
                    currentCamera.m_XAxis.m_InputAxisName = "";
                    currentCamera.m_YAxis.m_InputAxisName = "";
                } else {
                    currentCamera.m_XAxis.m_InputAxisName = "Mouse X";
                    currentCamera.m_YAxis.m_InputAxisName = "Mouse Y";
                }
            }
        }
    }

    public static ConfirmationWindow RequestConfirmation(ConfirmationPromptData _data) {
        Instance.confirmationWindow.gameObject.SetActive(true);
        Instance.confirmationWindow.Unpack(_data);
        return Instance.confirmationWindow; //Allow calling object to subscribe to the result
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
        if (Input.GetButtonDown("Kwit")) { //
            ShowLappyMenu();
        }
    }
//Save / Load
    public void SaveGameData(int fileNum) {
        Debug.Log("Game Saved: "+Application.persistentDataPath);
        OnSave?.Invoke(fileNum);
    }

    public void LoadGameData(int fileNum) {
        Debug.Log("Game Loaded");
        OnLoad?.Invoke(fileNum);
    }
}

public static class ScreenSpace
{
    public static float Width = 3840f;
    public static float Height = 2160f;
	public static float Convert(float variable) {
		return variable * (Screen.height / Height);
	}

    public static float Inverse(float variable) {
		return variable * (Height / Screen.height);
	}
}