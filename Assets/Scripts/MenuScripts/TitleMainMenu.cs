using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class TitleMainMenu : MonoBehaviour {
    public Canvas MainMenu;
    public Canvas OptionMenu;
    public GameObject PersistenceGamePrefab;
    public GameObject PersistenceGameObject;

    void Start () {
        if (GameObject.FindGameObjectsWithTag("PersistentObject").Length < 1) {
            PersistenceGameObject = Instantiate(PersistenceGamePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        //Debug.Log(PersistenceGameObject);
        PersistenceGameObject.GetComponent<PeristentObject>().DebugProps();
        SetValuesFromGameSaveSettings();
        MainMenu = MainMenu.GetComponent<Canvas>();
        OptionMenu = OptionMenu.GetComponent<Canvas>();
        OptionMenu.gameObject.SetActive(false);
    }
	
    public void StartGame() {
        DontDestroyOnLoad(PersistenceGameObject);
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
        PersistenceGameObject.GetComponent<PeristentObject>().graphicsVal = QualitySettings.GetQualityLevel();
        foreach (Text GraphicsSliderText in GraphicsSliderTexts)
        {
            if (GraphicsSliderText.name == "CurrentSetting")
            {
                GraphicsSliderText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];//set from quality settings
            }
        }

        SetMusicLevel(PersistenceGameObject.GetComponent<PeristentObject>().musicVal);
        GetComponent<AudioManager>().SetMusicLevel(PersistenceGameObject.GetComponent<PeristentObject>().musicVal);
        GameObject MusicSlider = GameObject.Find("MusicSlider");
        MusicSlider.GetComponentInChildren<Slider>().value = PersistenceGameObject.GetComponent<PeristentObject>().musicVal;

        SetSFXLevel(PersistenceGameObject.GetComponent<PeristentObject>().sfxVal);
        GetComponent<AudioManager>().SetSFXLevel(PersistenceGameObject.GetComponent<PeristentObject>().sfxVal);
        GameObject SoundSlider = GameObject.Find("SoundSlider");
        SoundSlider.GetComponentInChildren<Slider>().value = PersistenceGameObject.GetComponent<PeristentObject>().sfxVal;
    }
    
    public void QuitGame() {
        //exit game
        Application.Quit();
    }

    public void SetSFXLevel(float sfxLvl)
    {
        PersistenceGameObject.GetComponent<PeristentObject>().sfxVal = sfxLvl;
        PersistenceGameObject.GetComponent<PeristentObject>().DebugProps();
    }

    public void SetMusicLevel(float musicLvl)
    {
        PersistenceGameObject.GetComponent<PeristentObject>().musicVal = musicLvl;
        PersistenceGameObject.GetComponent<PeristentObject>().DebugProps();
    }
}
