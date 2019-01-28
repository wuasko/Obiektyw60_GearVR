using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {

    public AudioClip hoverSound;
    public AudioClip clickSound;
    private AudioSource audioSource;
	
    // Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
	}

    public void buttonClick()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void buttonHover()
    {
        audioSource.PlayOneShot(hoverSound);
    }

}
