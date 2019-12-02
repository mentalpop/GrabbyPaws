using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{

    public List<Transform> targets;
    public Transform selectedTarget;

    private Transform myTransform;
    public float radius;
    public float depth;
    public float angle;
    private Physics physics;

    void Start() {
        targets = new List<Transform>();
        selectedTarget = null;
        myTransform = transform;

        AddAllTargets();
    }

    public void AddAllTargets() {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Targets");

        foreach (GameObject target in go)
            AddTarget(target.transform);
    }

    public void AddTarget(Transform target) {
        targets.Add(target);
    }

    private void SortTargetsByDistance() {
        targets.Sort(delegate (Transform t1, Transform t2) {
            return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
            });
    }

    private void SelectTarget(bool increaseIndex) {
        if(selectedTarget == null) {
            SortTargetsByDistance();
            selectedTarget = targets[0];
        } else {
            int index = targets.IndexOf(selectedTarget);

            /*
            if (increaseIndex)
                index++;
            else
                index--;
            index = Mathf.Clamp(index, 0, targets.Count - 1);
            //*/

            //*
            if(index < targets.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;

            }
            //*/
            selectedTarget = targets[index];
        }
    }

    void Update() {
        if(Input.GetButtonDown("ChangeFocus")) {
            SelectTarget(true);
        }

        /*
        if (Input.GetAxis("Mouse ScrollWheel") > 0f )  {// forward
            SelectTarget(true);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0f )  {// backwards
            SelectTarget(false);
        }
        //*/
    }
}
