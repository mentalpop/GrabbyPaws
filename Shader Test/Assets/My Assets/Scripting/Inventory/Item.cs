using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool consumable = false;
    public GameObject model = null;
    public float itemYOffset;
    public float itemXOffset;
    public float itemScale;
    public int value;
    public GameObject physicalItem;
    public int weight;

    [TextArea(3,10)]
    public string description = "Item Description";

    public virtual void Use()
    {
        //Use the item
        Debug.Log("Using" + name);
    }

}
