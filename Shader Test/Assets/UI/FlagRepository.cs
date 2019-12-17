using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class FlagRepository : Singleton<FlagRepository>
{
    public FlagList questFlags;
    public FlagList secretFlags;
    public GameFlags flags = new GameFlags();

    private void Awake() {
    //Initialize each to 0
        foreach (var flag in questFlags.flags) {
            flags.questFlags.Add(flag, 0);
        }
        foreach (var flag in secretFlags.flags) {
            flags.secretFlags.Add(flag, 0);
        }
    }
//Get / Set
    public static int ReadQuestKey(string key) {
        instance.flags.questFlags.TryGetValue(key, out int readValue);
        return readValue;
    }

    public static void WriteQuestKey(string key, int value) {
        instance.flags.questFlags[key] = value;
    }
//Secret Key
    public static int ReadSecretKey(string key) {
        instance.flags.secretFlags.TryGetValue(key, out int readValue);
        return readValue;
    }

    public static void SecretKeyFound(string key) { //"Find" a secret; set it's state to 1 if it isn't 2
        if (instance.flags.secretFlags.TryGetValue(key, out int readValue)) {
            if (readValue == 0)
                instance.flags.secretFlags[key] = 1;
        }
    }
    public static void SecretKeyStrike(string key) { //Strike a secret from the list, but only if it has been found
        if (instance.flags.secretFlags.TryGetValue(key, out int readValue)) {
            if (readValue == 1)
                instance.flags.secretFlags[key] = 2;
        }
    }

    public static void WriteSecretKey(string key, int value) {
        instance.flags.secretFlags[key] = value;
    }
//Save / Load
    private void OnEnable() {
        RegisterSingleton (this);
        UI.instance.OnSave += Save;
        UI.instance.OnLoad += Load;
    }

    private void OnDisable() {
        UI.instance.OnSave -= Save;
        UI.instance.OnLoad -= Load;
    }

    private string saveString = "flags";

    public void Save(int fileIndex) {
        ES3.Save<GameFlags>(saveString, flags);
    }

    public void Load(int fileIndex) {
        flags = ES3.Load(saveString, new GameFlags());
    }
}

public class GameFlags
{
    public Dictionary<string, int> questFlags = new Dictionary<string, int>();
    public Dictionary<string, int> secretFlags = new Dictionary<string, int>();
}