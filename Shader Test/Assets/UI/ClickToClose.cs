using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ClickToClose : MonoBehaviour, IPointerClickHandler
{
    public delegate void CloseEvent();
    public CloseEvent OnClick;

    private void OnEnable() {
        GetComponent<RectTransform>().sizeDelta = new Vector2(ScreenSpace.Width, ScreenSpace.Height);
    }

    public void OnPointerClick (PointerEventData evd) {
		OnClick?.Invoke();
	}
}