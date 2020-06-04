using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMiscTab : MonoBehaviour
{
    public Slider mouseSensitivity;

    private void OnEnable() {
        mouseSensitivity.value = UI.Instance.mouseSensitivity;
        mouseSensitivity.onValueChanged.AddListener (delegate {mouseSensitivity_onValueChanged ();});
    }

    private void OnDisable() {
        mouseSensitivity.onValueChanged.RemoveListener (delegate {mouseSensitivity_onValueChanged ();});
    }

    public void mouseSensitivity_onValueChanged() {
        UI.Instance.mouseSensitivity = mouseSensitivity.value;
    }
}