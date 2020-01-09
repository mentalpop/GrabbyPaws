using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public ButtonGeneric closeButton;
    public ClickToClose clickToClose;

	public GameObject lineItemPrefab;
	public Transform lineItemTransform;

	private void OnEnable() {
        clickToClose.OnClick += Close;
		closeButton.OnClick += Close;
	}

	private void OnDisable() {
        clickToClose.OnClick -= Close;
		closeButton.OnClick -= Close;
	}

	private void Close() {
		gameObject.SetActive(false);
	}
}