using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour {

    public Canvas PauseMenuView;
    public Canvas OptionMenuView;
    public Canvas QuitConfirmView;
    public Canvas ReturnMainMenuView;

    public GameObject PersistenceGamePrefab;
    GameObject PersistentGameObject;
    //public AudioSource gameMusic; // TODO: add music
    bool pauseState = false;
    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("PersistentObject").Length < 1)
        {
            PersistentGameObject = Instantiate(PersistenceGamePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else {
            PersistentGameObject = GameObject.FindGameObjectWithTag("PersistentObject");
        }
        PauseMenuView = PauseMenuView.GetComponent<Canvas>();
        OptionMenuView = OptionMenuView.GetComponent<Canvas>();
        QuitConfirmView = QuitConfirmView.GetComponent<Canvas>();
        ReturnMainMenuView = ReturnMainMenuView.GetComponent<Canvas>();
        SetValuesFromGameSaveSettings();//Set values from game/save settings
        PauseMenuView.gameObject.SetActive(true);
        OptionMenuView.gameObject.SetActive(false);
        QuitConfirmView.gameObject.SetActive(false);
        ReturnMainMenuView.gameObject.SetActive(false);
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

    public void Toggle(bool newPauseState) {
        if (newPauseState)
        {
            Time.timeScale = 0;//pause game
            GetComponent<CanvasGroup>().alpha = 1;
        }
        else {
            Time.timeScale = 1;
            GetComponent<CanvasGroup>().alpha = 0;
        }
        GameObject.FindGameObjectWithTag("GUI").GetComponent<CanvasGroup>().alpha = (newPauseState == false ? 1 : 0);
        pauseState = newPauseState;
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
        Toggle(false);
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
    public void SwitchToReturnMainMenu()
    {
        HideAllPauseMenuScreens();
        ReturnMainMenuView.gameObject.SetActive(true);
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
        PersistentGameObject.GetComponent<PeristentObject>().sfxVal = newVal;
        //PersistentGameObject.GetComponent<PeristentObject>().DebugProps();
    }

    public void AdjustMusicVolume()
    {
        float newVal = GameObject.Find("MusicSlider").GetComponentInChildren<Slider>().value;
        PersistentGameObject.GetComponent<PeristentObject>().musicVal = newVal;
        //PersistentGameObject.GetComponent<PeristentObject>().DebugProps();
    }

    public void returnToMainMenu()
    {
        DontDestroyOnLoad(PersistentGameObject);
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
