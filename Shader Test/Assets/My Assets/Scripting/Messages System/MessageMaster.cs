using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageMaster : MonoBehaviour {


    private Queue<string> rivenMessages;

    public RivenDialog[] rivenDialogs;
    public Message[] messages;


    public Text rivenText;
    // Use this for initialization
    void Start () {

        rivenMessages = new Queue<string>();
        if (rivenText != null)
        {
            StartRiven(0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DisplayNextSentence();
        }
	}

    void StartRiven(int d)
    {
        rivenMessages.Clear();

        foreach(string msg in rivenDialogs[d].sentences)
        {
            rivenMessages.Enqueue(msg);
        }
        DisplayNextSentence();
        
    }

    void DisplayNextSentence()
    {
        if(rivenMessages.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = rivenMessages.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    void EndDialogue()
    {
        rivenText.text = "";
    }

    IEnumerator TypeSentence(string sentence)
    {
        rivenText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            rivenText.text += letter;
            yield return null;
        }
    }


}
