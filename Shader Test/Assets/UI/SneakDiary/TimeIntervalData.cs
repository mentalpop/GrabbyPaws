using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeInterval", menuName = "SneakDiary/Time Interval", order = 1)]
public class TimeIntervalData : ScriptableObject
{
    public QuestNames questName;
    public string title;
    [TextArea(3, 10)]
    public string description;
    public bool isMajorEvent;
}