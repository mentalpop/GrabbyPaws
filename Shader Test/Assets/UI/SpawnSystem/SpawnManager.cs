using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefabPlayer;
    public GameObject prefabDialogueSystem;
    public GameObject prefabCinemachine;
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    [HideInInspector] public GameObject player;

    private void Awake() {
        SpawnPlayer(SpawnPoints.UITestRoomA);
    }

    public void SpawnPlayer(SpawnPoints point) {
        SpawnPoint pointToSpawnPlayer = spawnPoints[0];
        bool foundSpawnPoint = false;
        foreach (var spawnPoint in spawnPoints) {
            if (spawnPoint.pointID == point) {
                pointToSpawnPlayer = spawnPoint;
                foundSpawnPoint = true;
                break;
            }
        }
        if (foundSpawnPoint) {
            player = Instantiate(prefabPlayer, pointToSpawnPlayer.transform.position, pointToSpawnPlayer.transform.localRotation);
        } else {
            Debug.Log("Failed to fing SpawnPoint: "+point);
        }
    }
}