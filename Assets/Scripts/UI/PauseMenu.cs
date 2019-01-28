using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject pausePanel;
    public GameObject confirmQuitPanel;
	public GameObject player;

	// Use this for initialization
	void Start () {
		pausePanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		// Pause game
		if (OVRInput.Get(OVRInput.Button.Two)||(Input.GetKeyDown(KeyCode.Space)))
		{
			if (!pausePanel.activeInHierarchy)
			{
				PauseGame();
				print("Pressed pause");
			}
			if (pausePanel.activeInHierarchy)
			{
				ContinueGame();
				print("pressed unpause");
			}
		}
	}

	private void PauseGame()
	{
		Time.timeScale = 0;
		pausePanel.SetActive(true);
		pausePanel.transform.position = player.transform.position + player.transform.forward * 1;
		//Disable scripts that still work while timescale is set to 0
	}
	private void ContinueGame()
	{
		Time.timeScale = 1;
		pausePanel.SetActive(false);
		//enable the scripts again
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

    public void openPanelPause()
    {
        pausePanel.SetActive(true);
        confirmQuitPanel.SetActive(false);
    }

    public void openPanelConfirmQuit()
    {
        pausePanel.SetActive(false);
        confirmQuitPanel.SetActive(true);
    }

}
