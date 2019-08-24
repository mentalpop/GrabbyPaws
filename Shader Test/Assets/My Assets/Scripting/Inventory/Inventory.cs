using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> items = new List<Item>();

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one inventory in scene");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    [SerializeField]
    public OnItemChanged OnItemChangedCallback;


    public int space = 9;

    

    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            return false;
        }

        items.Add(item);

        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {

        items.Remove(item);

        if (OnItemChangedCallback != null)
        {

            OnItemChangedCallback.Invoke();
        }
        
    }


    public int ReturnValues()
    {
       
        int myValue = 0;
        for(int i = 0; i < items.Count; i++)
        {
            myValue += items[i].value;
            

        }

        int b = items.Count;

        for(var i = 0; i < b; i++)
        {
            FindObjectOfType<ThieveControl>().collectedItems.Add(items[0]);
            items.Remove(items[0]);
        }
        return myValue;
    }


    public float ReturnWeights()
    {
        float weight = 0;
        for(int i = 0; i < items.Count; i++)
        {
            weight += items[i].weight;
        }
        return weight;
    }
}
