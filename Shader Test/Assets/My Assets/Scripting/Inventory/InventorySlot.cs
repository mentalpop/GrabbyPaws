using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    Item item;
    public Image icon;
    //public Button removeButton;

    [SerializeField]
    public ToolTip tip;
    public GameObject modelTransform;
    private GameObject model = null;



    public void Awake()
    {
  

        tip = FindObjectOfType<ToolTip>();
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;

        if (model == null)
        {
            model = Instantiate(item.model, modelTransform.transform);
            model.transform.localPosition = new Vector3(0 + item.itemXOffset, 0 + item.itemYOffset, 0);
            model.layer = 5;
            model.transform.localScale = new Vector3(item.itemScale, item.itemScale, item.itemScale);
            if (model.GetComponent<Rigidbody>() != null)
            {
                model.GetComponent<Rigidbody>().useGravity = false;
            }


        }
        icon.enabled = true;
        //removeButton.interactable = true;
        

    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        Destroy(model);
        model = null;
        //removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        //Inventory.instance.Remove(item);
    }


    public void UseItem()
    {
        if(item != null && !tip.gameObject.activeSelf)
        {
            tip.ToggleMe(item, transform, this);
            tip.gameObject.SetActive(true);
            Debug.Log("Selected");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
        }
    }
}
