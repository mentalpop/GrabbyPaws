using UnityEngine;

public enum CatrgoryItem { Junk, Scrap, Key}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public CatrgoryItem category;
    public Sprite icon = null;
    //public bool isDefaultItem = false;
    public float value = 1f;
    public GameObject physicalItem;
    public float weight = 0f;
    public GameObject model = null;
    public Vector3 itemPositionOffset; //Position offset for the model, only x / y are actually used
    public Vector3 itemRotationOffset; //Euler angles for the rotation of the model
    public float itemScale = 2f;
    public bool consumable = false;

    [TextArea(3,10)]
    public string description = "Item Description";

    public virtual void Use()
    {
        //Use the item
        Debug.Log("Using" + name);
    }

}
