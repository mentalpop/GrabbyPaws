using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefabPlayer;
    public GameObject prefabDialogueSystem;
    public GameObject prefabCinemachine;
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject dSystem;

    /*
    private void Awake() {
        SpawnPlayer(SpawnPoints.UITestRoomA);
    }
    //*/

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
            dSystem = Instantiate(prefabDialogueSystem, Vector3.zero, Quaternion.identity);
    //Spawn the player
            player = Instantiate(prefabPlayer, pointToSpawnPlayer.transform.position, pointToSpawnPlayer.transform.localRotation);
            GameObject newGO = Instantiate(prefabCinemachine, pointToSpawnPlayer.transform.position, Quaternion.identity);
            vThirdPersonCamera tpCamera = newGO.GetComponent<vThirdPersonCamera>();
            tpCamera.SetTarget(player.transform);
    //Set the camera on the UI
            UI.Instance.cBrain = newGO.GetComponent<CinemachineBrain>();
            //UI.Instance.thirdPersonCamera = tpCamera;
        } else {
            Debug.Log("Failed to fing SpawnPoint: "+point);
        }
    }
}