using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakDiary : MonoBehaviour
{
    public ButtonGeneric closeButton;
		
	private void OnEnable() {
		closeButton.OnClick += OnClick;
	}

	private void OnDisable() {
		closeButton.OnClick -= OnClick;
	}

	private void OnClick() {
		gameObject.SetActive(false);
	}

    void Start() {

    }

    void Update() {

    }
}