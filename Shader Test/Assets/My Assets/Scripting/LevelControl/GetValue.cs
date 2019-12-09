using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValue : MonoBehaviour
{

    private Inventory i;
    public ThieveControl t;
    // Start is called before the first frame update
    void Start()
    {
        i = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetButtonDown("Steal"))
            {
                Debug.Log("Hey");
                t.totalValue += 1;//i.ReturnValues(); //Getting rid of this
                FindObjectOfType<InventoryUI>().UpdateUI();
            }
        }
    }



}
