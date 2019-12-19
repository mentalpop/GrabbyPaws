using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PixelCrushers.DialogueSystem;

public class TimeInterval : MonoBehaviour, IPointerClickHandler
{
    public QuestNames questNames;
    //public GameObject go;

    //private bool myBool;

    void Start() {

    }

    void Update() {

    }

    public void OnPointerClick(PointerEventData eventData) {
        var quests = QuestLog.GetAllQuests(QuestState.Abandoned | QuestState.Active | QuestState.Failure | QuestState.Success | QuestState.Unassigned, false);
        Debug.Log("quests: "+quests.Length);
        foreach (var quest in quests) {
            Debug.Log("quest: "+quest);
        }
        //QuestLog.CompleteQuest(QuestNames.q001TwilightCottonCandy.ToString());
    }
}