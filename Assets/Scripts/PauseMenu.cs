using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public Canvas PauseMenuView;
    public Canvas OptionMenuView;
    public Canvas QuitConfirmView;
    // Use this for initialization
    void Start () {
        Time.timeScale = 0;//pause game
        PauseMenuView = PauseMenuView.GetComponent<Canvas>();
        OptionMenuView = OptionMenuView.GetComponent<Canvas>();
        QuitConfirmView = QuitConfirmView.GetComponent<Canvas>();
        PauseMenuView.gameObject.SetActive(true);
        OptionMenuView.gameObject.SetActive(false);
        QuitConfirmView.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void ResumeGame() {
        gameObject.SetActive(false);
        Time.timeScale = 1;//resume game
    }

    public void SwitchToQuitConfirmation() {
        PauseMenuView.gameObject.SetActive(false);
        OptionMenuView.gameObject.SetActive(false);
        QuitConfirmView.gameObject.SetActive(true);
    }

    public void SwitchToPauseMenu()
    {
        PauseMenuView.gameObject.SetActive(true);
        OptionMenuView.gameObject.SetActive(false);
        QuitConfirmView.gameObject.SetActive(false);
    }

    public void SwitchToOptionsMenu()
    {
        PauseMenuView.gameObject.SetActive(false);
        OptionMenuView.gameObject.SetActive(true);
        QuitConfirmView.gameObject.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
