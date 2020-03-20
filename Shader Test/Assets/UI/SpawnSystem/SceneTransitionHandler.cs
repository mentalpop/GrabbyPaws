using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.SceneManagement;


public class SceneTransitionHandler : Singleton<SceneTransitionHandler>
{
    public SpawnPoints spawnPoint;
    [HideInInspector] public SpawnManager spawnManager;

    private void OnEnable() {
        RegisterSingleton(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        RegisterSingleton(this);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
    //Find SpawnManager
        spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager == null) {
            Debug.Log("SpawnManager not found in: "+scene.name);
        } else {
            spawnManager.SpawnPlayer(spawnPoint);
        }
    }

    public static void SceneGoto(string sceneName, SpawnPoints point) {
        SceneManager.LoadScene(sceneName);
        instance.spawnPoint = point;
    }
}