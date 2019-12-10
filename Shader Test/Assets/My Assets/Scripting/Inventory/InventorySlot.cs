using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    public float itemSpinSpeed = 72f;
    public Transform cube;
    //public Image icon;
    //public Button removeButton;

    //public ToolTip tip;
    //public GameObject modelTransform;
    private GameObject model;
    private Quaternion initialRotation;
    private Item item;
    private bool mouseOver = false;

    void Update() {
        if (mouseOver)
            model.transform.Rotate(0, Time.deltaTime * itemSpinSpeed, 0);
    }

    public void Unpack(Item _item) {
        item = _item;
        //* Going to use this later; 
        model = Instantiate(item.model, cube);
        //Debug.Log("cube transform: " + cube);
        model.transform.localPosition = item.positionOffset;
        initialRotation = Quaternion.Euler(item.rotationOffset);
        model.transform.rotation = initialRotation;
        model.transform.localScale = new Vector3(item.itemScale, item.itemScale, item.itemScale);
        model.layer = 5; //UI
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
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
//Tooltip Handling
        mouseOver = false;
        model.transform.rotation = initialRotation;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (item.category == CategoryItem.Trash) {
                Inventory.instance.Drop(item);
                Debug.Log("Dropping: " + item.name);
            } else {
                Debug.Log("Can't drop item: " + item.name);
            }
        }
    }
}