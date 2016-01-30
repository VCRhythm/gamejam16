using UnityEngine;
using System.Collections;

public class TitleMainMenu : MonoBehaviour {
    public Canvas MainMenu;
    public Canvas OptionMenu;
	// Use this for initialization
	void Start () {
        MainMenu = MainMenu.GetComponent<Canvas>();
        OptionMenu = OptionMenu.GetComponent<Canvas>();
        OptionMenu.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame() {
        //add code to swtich to scene
    }

    public void SwitchToOptionsScreen() {
        MainMenu.gameObject.SetActive(false);
        OptionMenu.gameObject.SetActive(true);
    }

    public void SwitchToMainScreen()
    {
        MainMenu.gameObject.SetActive(true);
        OptionMenu.gameObject.SetActive(false);
    }

    public void QuitGame() {
        //exit game
        Application.Quit();
    }
}
