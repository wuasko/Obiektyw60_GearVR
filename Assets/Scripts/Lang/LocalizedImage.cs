using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedImage : MonoBehaviour {

    public Sprite polishImage;
    public Sprite englishImage;
    

	// Use this for initialization
	void Start () {
        ReloadImage();
	}
	

	public void ReloadImage () {
        if (PlayerPrefs.GetInt("lang") == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = polishImage;
            print("Image should be polish");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = englishImage;
            print("Image should be english");
        }
	}
}
