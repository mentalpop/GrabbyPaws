using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlagList", menuName = "Lists/Flag List", order = 1)]
public class FlagList : ScriptableObject
{
    public List<string> flags = new List<string>();

    /*
    public int GetIndex(Item item) {
        for (int i = 0; i < items.Count; i++) {
            if (item == items[i]) {
                return i;
            }
        }
        Debug.LogWarning("Could not find item in list: "+item.name);
        return -1;
    }

    public Item GetItem(int index) {
        if (index <= items.Count)
            return items[index];
        Debug.LogWarning("Could not find item in list with index: "+index);
        return null;
    }
    //*/
}