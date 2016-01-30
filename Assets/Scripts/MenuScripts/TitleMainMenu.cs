using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMainMenu : MonoBehaviour {
    public Canvas MainMenu;
    public Canvas OptionMenu;
    //public AudioSource gameMusic; // TODO: add music

    void Start () {
        MainMenu = MainMenu.GetComponent<Canvas>();
        OptionMenu = OptionMenu.GetComponent<Canvas>();
        SetValuesFromGameSaveSettings();
        OptionMenu.gameObject.SetActive(false);
    }
	
    public void StartGame() {
        SceneManager.LoadSceneAsync(1);
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

    public void AdjustGraphicsSettings()
    {
        GameObject GraphicsSlider = GameObject.Find("GraphicsSlider");
        int newVal = (int)GraphicsSlider.GetComponentInChildren<Slider>().value;
        QualitySettings.SetQualityLevel(newVal);
        Text[] GraphicsSliderTexts = GraphicsSlider.GetComponentsInChildren<Text>();
        foreach (Text GraphicsSliderText in GraphicsSliderTexts)
        {
            if (GraphicsSliderText.name == "CurrentSetting")
            {
                GraphicsSliderText.text = QualitySettings.names[newVal];
            }
        }
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
        //exit game
        Application.Quit();
    }
}
