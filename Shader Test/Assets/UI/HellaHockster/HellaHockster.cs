using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;
using UnityEngine;

public class HellaHockster : MonoBehaviour
{
    public Inventory inventory;

    public ButtonGeneric closeButton;
    public CloseOnDeselect clickToClose;
    public ButtonOmni hocksterCallButton;
	public int availableHocksters = 2;
	public List<GameObject> hocksterImages = new List<GameObject>();
	public HocksterScrollRect inventoryRect;

	private void OnEnable() {
		hocksterCallButton.OnClick += HocksterCallButton_OnClick;
		clickToClose.OnDeselected += Close;
		closeButton.OnClick += Close;
        inventory.OnItemChanged += UpdateDisplay;
		UpdateHockstersAvailable();
		UpdateDisplay();
	}

	private void OnDisable() {
		hocksterCallButton.OnClick -= HocksterCallButton_OnClick;
        clickToClose.OnDeselected -= Close;
        inventory.OnItemChanged -= UpdateDisplay;
		closeButton.OnClick -= Close;
	}

	private void Close() {
		gameObject.SetActive(false);
	}

	private void HocksterCallButton_OnClick(bool _stateActive) {
		availableHocksters--;
		UpdateHockstersAvailable();
	}

	public void UpdateHockstersAvailable() {
		for (int i = 0; i < hocksterImages.Count; i++) {
			hocksterImages[i].SetActive(i < availableHocksters);
		}
	}

	private void UpdateDisplay() {
		inventoryRect.Unpack(inventory.items);
	}

	/*
	public void SpawnSecrets() {
		foreach(Transform child in lineItemTransform)
			Destroy(child.gameObject);
		List<NotSecretLineItem> lineItems = new List<NotSecretLineItem>();
		foreach (var secret in FlagRepository.instance.secretFlags.secrets) {
			//Debug.Log("secret.secret.ToString(): "+secret.secret.ToString());
			if (FlagRepository.ReadSecretKey(secret.secret.ToString()) != 0) { //If the secret has been discovered
				GameObject newGO = Instantiate(lineItemPrefab, lineItemTransform, false);
				NotSecretLineItem notSecretLineItem = newGO.GetComponent<NotSecretLineItem>();
				notSecretLineItem.Unpack(secret);
				lineItems.Add(notSecretLineItem);
			}
		}
	//Move Stricken items to the bottom of the list
		foreach (var item in lineItems) {
			if (item.isStricken) {
				item.gameObject.transform.SetAsLastSibling();
			}
		}
	}
	//*/
}