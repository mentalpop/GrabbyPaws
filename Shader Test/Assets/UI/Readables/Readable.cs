using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Readable : MonoBehaviour
{
    public ClickToClose clickToClose;
    public TextMeshProUGUI bookTitle;
    public TextMeshProUGUI bookText;

    private void OnEnable() {
        clickToClose.OnClick += Close;
    }

    private void OnDisable() {
        clickToClose.OnClick -= Close;
    }

    public void Unpack(ReadableData rData) {
        bookTitle.text = rData.title;
        bookText.text = rData.contents;
    }

    public void Close() {
        gameObject.SetActive(false); //For now, just close instantly
    }
}