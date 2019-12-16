﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Inventory : Singleton<Inventory>//, IFileIO<List<int>>
{
    public List<Item> items = new List<Item>();
    public ItemMetaList itemMetaList;
    public Vector3 dropPosition;
    //public UI uiRef;

    private string saveString = "inventory";
    /*
    #region Singleton
    public static Inventory instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one inventory in scene");
            return;
        }
        instance = this;
    }
    #endregion
    //*/

    public delegate void InventoryEvent();
    public InventoryEvent OnItemChanged;
    
    private void OnEnable() {
        RegisterSingleton (this);
        UI.instance.OnSave += Save;
        UI.instance.OnLoad += Load;
    }

    private void OnDisable() {
        UI.instance.OnSave -= Save;
        UI.instance.OnLoad -= Load;
    }

    public void Save(int fileIndex) {
        List<int> itemIDs = new List<int>();
        foreach (var item in items) {
            itemIDs.Add(itemMetaList.GetIndex(item));
        }
        ES3.Save<List<int>>(saveString, itemIDs);
    }

    public void Load(int fileIndex) {
        List<int> loadItems = ES3.Load(saveString, new List<int>());
        items.Clear();
        foreach (var item in loadItems) {
            items.Add(itemMetaList.GetItem(item));
        }
    }

    public bool Add(Item item) {
        /* No space limit, so don't return false
        if (items.Count >= space) {
            return false;
        }
        //*/

        items.Add(item);

        OnItemChanged?.Invoke();
        return true;
    }

    public void Remove(Item item) {

        items.Remove(item);
        
        OnItemChanged?.Invoke();
        
    }

    public GameObject Drop(Item _toDrop) {
        GameObject toDrop = null;
//Drop an item from the Inventory
        foreach (var item in items) { //Ensure the item exists in the inventory
            if (item == _toDrop) {
                if (item.physicalItem != null) {
                    toDrop = Instantiate(item.physicalItem, dropPosition, Quaternion.identity);
                }
                Remove(item);
                break;
            }
        }
        return toDrop;
    }


    /* This is a disaster, removing it
    public int ReturnValues() {

        ThieveControl controller = FindObjectOfType<ThieveControl>();
        int myValue = 0;

        foreach (var item in items)
        {
            myValue += item.value;
            controller.collectedItems.Add(item);
        }
        items.Clear();
        return myValue;
    }
    //*/


    public float ReturnWeights() {
        float weight = 0;
        for(int i = 0; i < items.Count; i++) {
            weight += items[i].weight;
        }
        return weight;
    }
}
