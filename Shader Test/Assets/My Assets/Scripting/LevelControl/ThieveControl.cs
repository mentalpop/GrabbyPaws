using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Invector.CharacterController;
using UnityEngine.SceneManagement;



public class ThieveControl : MonoBehaviour
{

    public float time = 0.0f;
    public Text t;
    public Text t2;
    private int lastMinute;
    public AudioSource tick;
    public int totalValue;

    private int valueActual = 0;
    //Player's total weight tolerance
    public float weightTotal = 100;
    public float currWeight;
    public vThirdPersonMotor playerMotor;
    public vThirdPersonController player;
    private bool endTrigger;

    public Text endText;

    public Text amtText;

    public int valueThreshold;

    public GameObject[] inRadius;
    public float interActionRadius;

    public List<Item> neededItems;
    public List<Item> collectedItems;


    // Start is called before the first frame update
    void Start()
    {
        lastMinute = 0;
        endTrigger = false;

        if (endText != null)
        {
            endText.text = "";
        }

        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()

    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (endTrigger == false)
            {
                if (checkItems())
                {
                    player.enabled = false;
                    endText.text = "Our house'll be loaded with treasures!";
                }
                else
                {
                    player.enabled = false;
                    endText.text = "Next time: more stuff!";
                }
                endTrigger = true;
            }

            if (Input.GetButtonDown("Kwit"))
            {
                SceneManager.LoadScene("HouseLevel");
            }
        }

        t.text = FormatSeconds(time);

        valueActual = (int)Mathf.Lerp(valueActual, totalValue, 0.3f);
        t2.text = "Total items Value: " + valueActual.ToString();

        currWeight = Inventory.instance.ReturnWeights();
        playerMotor.weightCoef = Mathf.Clamp(1.0f - (currWeight / weightTotal), 0, 1.0f);
        amtText.text = Inventory.instance.items.Count.ToString() + "/" + Inventory.instance.items.Count;//.space;

 

    }

    string FormatSeconds(float elapsed)
    {

        int d = (int)(elapsed * 100.0f);
        int minutes = d / (60 * 100);


        int seconds = (d % (60 * 100)) / 100;
        if (seconds != lastMinute)
        {
            tick.Play();
            lastMinute = seconds;
        }
        int hundredths = d % 100;
        return String.Format("{0:00}:{1:00}", minutes, seconds);
    }


    bool checkItems()
    {
        Inventory inv = Inventory.instance;

        bool bb = false;
        for(int b = 0; b < neededItems.Count; b++)
        {
            bb = collectedItems.Contains(neededItems[b]);

        }
        return bb;
    }





}
