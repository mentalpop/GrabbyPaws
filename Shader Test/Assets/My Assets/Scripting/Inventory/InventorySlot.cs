using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    public Transform cube;
    //public Image icon;
    //public Button removeButton;

    //public ToolTip tip;
    //public GameObject modelTransform;
    private GameObject model;// = null;
    private Item item;

    public void Unpack(Item _item) {
        item = _item;
        //* Going to use this later; 
        model = Instantiate(item.model, cube);
        //Debug.Log("cube transform: " + cube);
        model.transform.localPosition = item.itemPositionOffset;// new Vector3(item.itemPositionOffset.x, item.itemPositionOffset.y, 0);
        model.transform.rotation = Quaternion.Euler(item.itemRotationOffset);
        model.transform.localScale = new Vector3(item.itemScale, item.itemScale, item.itemScale);
        model.layer = 5;
        //Debug.Log("item.itemScale" + ": " + item.itemScale);
        /*
        if (model.GetComponent<Rigidbody>() != null) {
            model.GetComponent<Rigidbody>().useGravity = false;
            //*/
        //*/
    }

    /*
    public void Awake()
    {
  

        tip = FindObjectOfType<ToolTip>();
    }
    //*/

    /*
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;

        if (model == null) {
            model = Instantiate(item.model, modelTransform.transform);
            model.transform.localPosition = new Vector3(0 + item.itemXOffset, 0 + item.itemYOffset, 0);
            model.layer = 5;
            model.transform.localScale = new Vector3(item.itemScale, item.itemScale, item.itemScale);
            if (model.GetComponent<Rigidbody>() != null) {
                model.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        icon.enabled = true;
        //removeButton.interactable = true;
        

    }
    //*/

    /*
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        Destroy(model);
        model = null;
        //removeButton.interactable = false;
    }
    //*/
    /*
    
    public void OnRemoveButton()
    {
        //Inventory.instance.Remove(item);
    }
    //*/


    public void UseItem()
    {
        /*
        if(item != null && !tip.gameObject.activeSelf)
        {
            tip.ToggleMe(item, transform, this);
            tip.gameObject.SetActive(true);
            Debug.Log("Selected");
        }
        //*/
    }

    public void OnPointerEnter(PointerEventData eventData) {
//Tooltip Handling
    }

    public void OnPointerExit(PointerEventData eventData) {
//Tooltip Handling
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (item.cursed) {
                Debug.Log("Can't drop cursed item: " + item.name);
            } else {
                Inventory.instance.Drop(item);
                Debug.Log("Dropping: " + item.name);
            }
        }
    }
}