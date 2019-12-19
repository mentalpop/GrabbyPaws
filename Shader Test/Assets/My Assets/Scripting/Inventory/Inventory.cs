using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using PixelCrushers.DialogueSystem;

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
        RegisterLuaFunctions();
    }

    private void OnDisable() {
        UI.instance.OnSave -= Save;
        UI.instance.OnLoad -= Load;
    }
    #region Lua Functions
    private void RegisterLuaFunctions() {
        Lua.RegisterFunction("InventoryHas", this, SymbolExtensions.GetMethodInfo(() => InventoryHas(string.Empty)));
        Lua.RegisterFunction("InventoryCount", this, SymbolExtensions.GetMethodInfo(() => InventoryCount(string.Empty)));
        Lua.RegisterFunction("InventoryAdd", this, SymbolExtensions.GetMethodInfo(() => InventoryAdd(string.Empty, 0)));
        Lua.RegisterFunction("InventoryRemove", this, SymbolExtensions.GetMethodInfo(() => InventorySubtract(string.Empty, 0)));
        Lua.RegisterFunction("InventoryRemoveAllOf", this, SymbolExtensions.GetMethodInfo(() => InventoryRemove(string.Empty)));
    }

    public bool InventoryHas(string name) {
        foreach (var item in items) { //Ensure the item exists in the inventory
            if (item.name == name) {
                return true;
            }
        }
        return false;
    }

    public double InventoryCount(string name) {
        int count = 0;
        foreach (var item in items) { //Count occurrences of the item in the inventory
            if (item.name == name) {
                count++;
            }
        }
        return count;
    }

    public void InventoryAdd(string name, int quantity) {
        foreach (var item in itemMetaList.items) { //Find the Item in the Meta list based on String reference, add X of it to the inventory
            if (item.name == name) {
                while (quantity > 0) {
                    items.Add(item);
                    Debug.Log("Adding to Inventory: " + item.name);
                    quantity--;
                }
                break;
            }
        }
        OnItemChanged?.Invoke();
    }

    public void InventorySubtract(string name, int quantity) {
        while (quantity > 0) {
            foreach (var item in items) { //Try to remove an item if it exists in the inventory
                if (item.name == name) {
                    items.Remove(item);
                    break;
                }
            }
            quantity--;
        }
        OnItemChanged?.Invoke();
    }

    public void InventoryRemove(string name) {
//Remove all occurrences of an item from the inventory, good if you don't want to be specific
        items.RemoveAll(item => item.name == name); //Remove everything that matches the name
        /*
        foreach (var item in items) { 
            if (item.name == name) {
                items.Add(item);
                Debug.Log("Removing from Inventory: " + item.name);
            }
        }
        //*/
        OnItemChanged?.Invoke();
    }
    
    #endregion
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
