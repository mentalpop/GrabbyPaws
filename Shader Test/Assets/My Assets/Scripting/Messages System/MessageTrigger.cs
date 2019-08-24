using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MessageTrigger : MonoBehaviour {



    private Text text;
    private Camera c;
    private int pos;
    public int message;
    private MessageMaster master;
    private string[] sentences;
    private bool canProgress;
	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
        text.text = "Hello!";

        c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        pos = 0;

        master = GameObject.FindGameObjectWithTag("Message Master").GetComponent<MessageMaster>();

        sentences = master.messages[message].text;

        text.text = sentences[0];
        pos = 0;
    }
	
	// Update is called once per frame
	void Update () {
        text.transform.rotation = Quaternion.LookRotation(c.transform.position - text.transform.position);
        text.transform.rotation = Quaternion.Euler(text.transform.rotation.eulerAngles.x + 180, text.transform.rotation.eulerAngles.y, text.transform.rotation.eulerAngles.z + 180);

        if (Input.GetKeyDown(KeyCode.E) && canProgress)
        {
            pos++;
            Debug.Log(pos);
            if (pos >= sentences.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                text.text = sentences[pos];
            }

        }
    }


    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canProgress = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canProgress = false;
        }
    }
}
