using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
	public int currentQuest = 0;
	float displayTime = 5f;
	public Quest[] quests;
	public GameObject questPanel;
	public Text questLabel;

	private bool isQuestDisplayed = false;

	// Use this for initialization
	void Start ()
	{
		int i = 0;
		foreach (Quest q in quests)
		{
			q.number = i++;
		}
		StartQuest(currentQuest);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isQuestDisplayed)
		{
			displayTime -= Time.deltaTime;

			//questLabel.text = "Time " + displayTime;
			if (displayTime < 0)
			{
				HideQuestPanel();
                displayTime = 5f;
			}
		}
	}

	private void HideQuestPanel()
	{
		questPanel.SetActive(false);
		isQuestDisplayed = false;
	}

	public void StartQuest(int questID)
	{
		questPanel.SetActive(true);
		isQuestDisplayed = true;
		switch (questID)
		{
			case 0:
				questLabel.text = "Take morning medicine Antemeridil from the night stand.";
				break;
			case 1:
				questLabel.text = "Find wand.";
				break;
			case 2:
				questLabel.text = "Take morning medicine Antemeridil from the night stand.";
				break;
			case 3:
				questLabel.text = "Eat something.";
				break;
			case 4:
				questLabel.text = "Take medicine  Prandium.";
				break;
			case 5:
				questLabel.text = "Brush teeth.";
				break;
			case 6:
				questLabel.text = "Find wallet.";
				break;
			case 7:
				questLabel.text = "Leave house.";
				break;
            case 8:
                questLabel.text = "Victory!";
                break;

		}
	}

	public void SetEndOfQuestByName(string targetQuestName)
	{
		foreach (Quest q in quests)
		{
			if ((q.targetTagName == targetQuestName) && (currentQuest == q.number) && (!q.completed))
			{
				q.completed = true;
                StartQuest(++currentQuest);
				break;
			}               
		}
	}
}
