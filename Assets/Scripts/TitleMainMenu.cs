﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
public class TitleMainMenu : MonoBehaviour {
    public Canvas MainMenu;
    public Canvas OptionMenu;
    public GameObject PersistenceGameObject;

    // Use this for initialization
    void Start () {
        MainMenu = MainMenu.GetComponent<Canvas>();
        OptionMenu = OptionMenu.GetComponent<Canvas>();
        SetValuesFromGameSaveSettings();
        OptionMenu.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame() {
        //add code to swtich to scene
        DontDestroyOnLoad(PersistenceGameObject);
        SceneManager.LoadScene(1);
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
    }
    
    public void QuitGame() {
        //exit game
        Application.Quit();
    }

    public void SetSFXLevel(float sfxLvl)
    {
        PersistenceGameObject.GetComponent<PeristentObject>().sfxVal = sfxLvl;
    }

    public void SetMusicLevel(float musicLvl)
    {
        PersistenceGameObject.GetComponent<PeristentObject>().musicVal = musicLvl;
    }
}
