using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour {

    public Canvas PauseMenuView;
    public Canvas OptionMenuView;
    public Canvas QuitConfirmView;
    //public AudioSource gameMusic; // TODO: add music

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0;//pause game
        PauseMenuView = PauseMenuView.GetComponent<Canvas>();
        OptionMenuView = OptionMenuView.GetComponent<Canvas>();
        QuitConfirmView = QuitConfirmView.GetComponent<Canvas>();
        SetValuesFromGameSaveSettings();//Set values from game/save settings
        PauseMenuView.gameObject.SetActive(true);
        OptionMenuView.gameObject.SetActive(false);
        QuitConfirmView.gameObject.SetActive(false);
    }

    public void AdjustGraphicsSettings()
    {
        GameObject GraphicsSlider = GameObject.Find("GraphicsSlider");
        int newVal = (int)GraphicsSlider.GetComponentInChildren<Slider>().value;
        QualitySettings.SetQualityLevel(newVal);
        Text[] GraphicsSliderTexts = GraphicsSlider.GetComponentsInChildren<Text>();
        foreach(Text GraphicsSliderText in GraphicsSliderTexts)
        {
            if (GraphicsSliderText.name == "CurrentSetting")
            {
                GraphicsSliderText.text = QualitySettings.names[newVal];
            }
        }
    }

	// Update is called once per frame
	void Update () {
    }

    void SetValuesFromGameSaveSettings()
    {
        //set text value
        GameObject GraphicsSlider = GameObject.Find("GraphicsSlider");
        Text[] GraphicsSliderTexts = GraphicsSlider.GetComponentsInChildren<Text>();
        GraphicsSlider.GetComponentInChildren<Slider>().value = QualitySettings.GetQualityLevel();//set from quality settings
        foreach (Text GraphicsSliderText in GraphicsSliderTexts)
        {
            if (GraphicsSliderText.name == "CurrentSetting")
            {
                GraphicsSliderText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];//set from quality settings
            }
        }
    }

    public void ResumeGame() {
        gameObject.SetActive(false);
        Time.timeScale = 1;//resume game
    }

    public void SwitchToQuitConfirmation()
    {
        HideAllPauseMenuScreens();
        QuitConfirmView.gameObject.SetActive(true);
    }

    public void SwitchToPauseMenu()
    {
        HideAllPauseMenuScreens();
        PauseMenuView.gameObject.SetActive(true);
    }

    public void SwitchToOptionsMenu()
    {
        HideAllPauseMenuScreens();
        OptionMenuView.gameObject.SetActive(true);
    }

    void HideAllPauseMenuScreens()
    {
        GameObject[] screens = GameObject.FindGameObjectsWithTag("PauseMenuScreen");
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }
    }

    public void AdjustSoundVolume()
    {
        float newVal = GameObject.Find("SoundSlider").GetComponentInChildren<Slider>().value;
        AudioListener.volume = newVal;
        //Debug.Log(newVal);
    }

    public void AdjustMusicVolume()
    {
        float newVal = GameObject.Find("MusicSlider").GetComponentInChildren<Slider>().value;
        //gameMusic.volume = newVal;
        //Debug.Log(newVal);
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}
