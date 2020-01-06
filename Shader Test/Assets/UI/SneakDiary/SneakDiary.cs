using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class SneakDiary : MonoBehaviour
{
    public ButtonGeneric closeButton;
    public ClickToClose clickToClose;
	
    public GameObject toolTipPrefab;
    public GameObject toolTipExpandedPrefab;
		
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

    /*
	void Start() {

    }

    void Update() {

    }
	//*/

	public GameObject TooltipOpenSmall(string text, Vector2 position, bool faceLeft) {
		GameObject newTooltip = Instantiate(toolTipPrefab, transform, false);
		newTooltip.transform.position = position;
		TooltipSmall ttS = newTooltip.GetComponent<TooltipSmall>();
		ttS.Unpack(text, faceLeft);
		return newTooltip;
	}

	public GameObject TooltipOpenLarge(TimeIntervalData timeIntervalData, Vector2 position, bool faceLeft) {
		GameObject newTooltip = Instantiate(toolTipExpandedPrefab, transform, false);
		newTooltip.transform.position = position;
		TooltipTextContainer ttC = newTooltip.GetComponent<TooltipTextContainer>();
		ttC.Unpack(timeIntervalData, faceLeft);
		return newTooltip;
	}
}