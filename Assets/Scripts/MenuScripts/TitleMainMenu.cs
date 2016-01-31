using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMainMenu : MonoBehaviour {
    public Canvas MainMenu;
    public Canvas OptionMenu;
    public PeristentObject persistenceGameObject;

    void Start ()
    {        
        SetValuesFromGameSaveSettings();
        MainMenu = MainMenu.GetComponent<Canvas>();
        OptionMenu = OptionMenu.GetComponent<Canvas>();
        OptionMenu.gameObject.SetActive(false);
    }
	
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToOptionsScreen()
    {
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
        persistenceGameObject.graphicsVal = QualitySettings.GetQualityLevel();
        foreach (Text GraphicsSliderText in GraphicsSliderTexts)
        {
            if (GraphicsSliderText.name == "CurrentSetting")
            {
                GraphicsSliderText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];//set from quality settings
            }
        }

        SetMusicLevel(persistenceGameObject.musicVal);
        GetComponent<AudioManager>().SetMusicLevel(persistenceGameObject.musicVal);
        GameObject MusicSlider = GameObject.Find("MusicSlider");
        MusicSlider.GetComponentInChildren<Slider>().value = persistenceGameObject.musicVal;

        SetSFXLevel(persistenceGameObject.sfxVal);
        GetComponent<AudioManager>().SetSFXLevel(persistenceGameObject.sfxVal);
        GameObject SoundSlider = GameObject.Find("SoundSlider");
        SoundSlider.GetComponentInChildren<Slider>().value = persistenceGameObject.sfxVal;
    }
    
    public void QuitGame() {
        //exit game
        Application.Quit();
    }

    public void SetSFXLevel(float sfxLvl)
    {
        persistenceGameObject.sfxVal = sfxLvl;
        //persistenceGameObject.DebugProps();
    }

    public void SetMusicLevel(float musicLvl)
    {
        persistenceGameObject.musicVal = musicLvl;
        //persistenceGameObject.DebugProps();
    }
}
