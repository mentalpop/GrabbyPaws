using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using PixelCrushers.DialogueSystem;

public class Inventory : Singleton<Inventory>//, IFileIO<List<int>>
{
    public List<InventoryItem> items = new List<InventoryItem>();
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
            if (item.item.name == name) {
                return true;
            }
        }
        return false;
    }

    public double InventoryCount(string name) {
        int count = 0;
        foreach (var item in items) { //Count occurrences of the item in the inventory
            if (item.item.name == name) {
                count = item.quantity;
                break;
            }
        }
        return count;
    }

    public void InventoryAdd(string name, int quantity) {
        foreach (var item in itemMetaList.items) { //Find the Item in the Meta list based on String reference, add X of it to the inventory
            if (item.name == name) {
                items.Add(new InventoryItem(item, quantity));
                Debug.Log("Adding to Inventory: " + item.name);
                /*
                while (quantity > 0) {
                    
                    quantity--;
                }
                //*/
                break;
            }
        }
        OnItemChanged?.Invoke();
    }

    public void InventorySubtract(string name, int quantity) {
        while (quantity > 0) {
            foreach (var item in items) { //Try to remove an item if it exists in the inventory
                if (item.item.name == name) {
                    while (quantity > 0) {
                        if (item.quantity > 0)
                            Remove(item.item);
                        else
                            Debug.LogWarning("Trying to remove more of an item from the Inventory than exists in the inventory");
                        quantity--;
                    }
                    break;
                }
            }
            quantity--;
        }
        OnItemChanged?.Invoke();
    }

    public void InventoryRemove(string name) {
//Remove all occurrences of an item from the inventory, good if you don't want to be specific
        foreach (var item in items) { 
            if (item.item.name == name) {
                RemoveAll(item.item);
                break;
            }
        }
        //items.RemoveAll(item => item.item.name == name); //Remove everything that matches the name
        OnItemChanged?.Invoke();
    }
    
    #endregion
    public void Save(int fileIndex) {
        List<IItemID> itemIDs = new List<IItemID>();
        foreach (var item in items) {
            itemIDs.Add(new IItemID(itemMetaList.GetIndex(item.item), item.quantity));
        }
        ES3.Save<List<int>>(saveString, itemIDs);
    }

    public void Load(int fileIndex) {
        List<IItemID> loadItems = ES3.Load(saveString, new List<IItemID>());
        items.Clear();
        foreach (var item in loadItems) {
            items.Add(new InventoryItem(itemMetaList.GetItem(item.itemID), item.quantity));
        }
    }

    public bool Add(Item item) {
//If you don't specify a quantity, add 1
        Add(item, 1);
        return true;
    }

    public bool Add(Item item, int quantity) {
//Check if the item is already in the inventory
        bool foundInInventory = false;
        foreach (var iItem in items) {
            if (iItem.item == item) {
        //Add to it's quantity
                iItem.quantity += quantity;
                foundInInventory = true;
            }
        }
        if (!foundInInventory) {
    //if there are none, add to the list instead
            items.Add(new InventoryItem(item, quantity));
        }
        OnItemChanged?.Invoke();
        return true;
    }

    public void Remove(Item item) {
//Check if the item is already in the inventory
        foreach (var iItem in items) {
            if (iItem.item == item) {
        //Remove one from it's quantity
                iItem.quantity -= 1;
                if (iItem.quantity < 1) {
            //If you have none left, actually remove the item
                    items.Remove(iItem);
                }
                break;
            }
        }
        OnItemChanged?.Invoke();
    }

    public void RemoveAll(Item item) {
//Check if the item is already in the inventory
        foreach (var iItem in items) {
            if (iItem.item == item) {
        //Remove all instances of it
                items.Remove(iItem);
                break;
            }
        }
        OnItemChanged?.Invoke();
    }

    public GameObject Drop(Item _toDrop) {
        GameObject toDrop = null;
//Drop an item from the Inventory
        foreach (var item in items) { //Ensure the item exists in the inventory
            if (item.item == _toDrop) {
                if (item.item.physicalItem != null) {
                    toDrop = Instantiate(item.item.physicalItem, dropPosition, Quaternion.identity);
                }
                Remove(item.item);
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
            weight += items[i].item.weight * items[i].quantity; //Return item multiplied by weight to account for Quantity
        }
        return weight;
    }
}

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int quantity;

    public InventoryItem(Item item, int quantity) {
        this.item = item;
        this.quantity = quantity;
    }
}

[System.Serializable]
public class IItemID
{
    public int itemID;
    public int quantity;

    public IItemID(int itemID, int quantity) {
        this.itemID = itemID;
        this.quantity = quantity;
    }
}