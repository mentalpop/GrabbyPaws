using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI weight;
    public TextMeshProUGUI value;
    public RectTransform myRect;
    public Vector2 canvasSize;
    public float offset = 16f;

    public void Unpack(InventoryItem inventoryItem, Vector2 _position) {
        itemName.text = inventoryItem.item.name;
        description.text = inventoryItem.item.description;
        weight.text = (inventoryItem.item.weight * inventoryItem.quantity).ToString();
        float _value = inventoryItem.item.value * inventoryItem.quantity;
        if (_value < 100f) {
    //Prepend zeroes in front of small numbers
            if (_value < 10f) {
                value.text = "00"+_value.ToString();
            } else {
                value.text = "0"+_value.ToString();
            }
        } else {
            value.text = string.Format("{0:n0}", _value);
        }
    //Keep on screen
        var correctedPosition = Camera.main.WorldToScreenPoint(_position);
        //Debug.Log("_position.y: "+(correctedPosition.y - 2160f / 2f));
        float yMax = (canvasSize.y - myRect.rect.height - offset) / 2f;
        myRect.anchoredPosition = new Vector3(Mathf.Clamp(correctedPosition.x, 0f, canvasSize.x - myRect.rect.width - offset), //
            Mathf.Clamp(correctedPosition.y - canvasSize.y / 2f, -yMax, yMax), transform.position.z);

    }
}