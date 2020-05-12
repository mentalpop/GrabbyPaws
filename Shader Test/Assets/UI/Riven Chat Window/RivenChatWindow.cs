using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivenChatWindow : MonoBehaviour
{
    public ButtonGeneric closeButton;
    public ClickToClose clickToClose;

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