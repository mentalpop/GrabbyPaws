using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoor : Interactable
{
    public string sceneName;
    public SpawnPoints destinationPoint;

    public override void Interact() {
        //Debug.Log("sceneName: "+sceneName);
        SceneTransitionHandler.SceneGoto(sceneName, destinationPoint);
    }
}