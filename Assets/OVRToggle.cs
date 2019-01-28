using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRToggle : MonoBehaviour {

	public bool value;
	public Image buttonSprite;
	public Sprite toggleOn;
	private Sprite toggleOff;

	// Use this for initialization
	void Start () {
		toggleOff = buttonSprite.sprite;
		if (value)
		{
			buttonSprite.sprite = toggleOn;
		}
		else
		{
			buttonSprite.sprite = toggleOff;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClickButton()
	{
		if (value)
		{
			value = false;
			buttonSprite.sprite = toggleOff;
		}
		else
		{
			value = true;
			buttonSprite.sprite = toggleOn;
		}
	}
}
