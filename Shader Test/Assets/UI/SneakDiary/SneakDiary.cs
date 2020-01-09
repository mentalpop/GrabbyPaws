using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakDiary : MonoBehaviour
{
    public ButtonGeneric closeButton;
    public ClickToClose clickToClose;
	
    public GameObject toolTipPrefab;
    public GameObject toolTipExpandedPrefab;

	[Range(0, 7)]
	public int timeInterval = 0;
	public float timelineSegmentWidth = 80f;
	public GameObject timelineStick;
	
    public NightPhases nightPhase = NightPhases.p1Twilight;
	public GameObject npcProfilePrefab;
    public Transform profileListTransform;
	public NPCListData npcList;

	//[HideInInspector] public string[] quests;
	private Vector2 timelineStickOrigin;

	private void OnEnable() {
        clickToClose.OnClick += Close;
		closeButton.OnClick += Close;
	}

	private void OnDisable() {
        clickToClose.OnClick -= Close;
		closeButton.OnClick -= Close;
	}

	private void Close() {
		gameObject.SetActive(false);
	}

	private void Start() {
	//Debug
		FlagRepository.WriteQuestKey(QuestNames.q001TwilightCottonCandyEndFlag.ToString(), true);
	//Spawn list of NPCs
		foreach (var npc in npcList.npcList) {
			GameObject npcProfile = Instantiate(npcProfilePrefab, profileListTransform, false);
			SneakDiaryProfile sneakDiaryProfile = npcProfile.GetComponent<SneakDiaryProfile>();
			sneakDiaryProfile.Unpack(npc, this);
		}
	//Timeline Stick
		timelineStickOrigin = timelineStick.transform.position;
	//Profiles will need access to the list of Quests, so collect them here for cached reference
        /*
        quests = QuestLog.GetAllQuests(QuestState.Abandoned | QuestState.Active | QuestState.Failure | QuestState.Success | QuestState.Unassigned, false);
        Debug.Log("# quests found by Sneak Diary: "+quests.Length);
		foreach (var quest in quests) {
            Debug.Log("quest: "+quest);
        }
		//*/
	}

	private void Update() {
		timelineStick.transform.position = new Vector2(timelineStickOrigin.x + ScreenSpace.Convert(timelineSegmentWidth) * timeInterval, timelineStickOrigin.y);
	}

	public GameObject TooltipOpenSmall(string text, Vector2 position, bool faceLeft) {
		GameObject newTooltip = Instantiate(toolTipPrefab, transform, false);
		newTooltip.transform.position = position;
		TooltipSmall ttS = newTooltip.GetComponent<TooltipSmall>();
		ttS.Unpack(text, faceLeft);
		return newTooltip;
	}

	public GameObject TooltipOpenLarge(TimeIntervalData timeIntervalData, Vector2 position, bool faceLeft) {
		GameObject newTooltip = Instantiate(toolTipExpandedPrefab, transform, false);
		newTooltip.transform.position = position;
		TooltipTextContainer ttC = newTooltip.GetComponent<TooltipTextContainer>();
		ttC.Unpack(timeIntervalData, faceLeft);
		return newTooltip;
	}
}