using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    //public AudioMixerSnapshot TitleMenuMusic;
    /*public AudioMixerSnapshot inCombat;
    public AudioClip[] stings;
    public AudioSource stingSource;
    public float bpm = 128;

    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;
    */

    public AudioMixer masterMixer;

    // Use this for initialization
    void Start()
    {
        /*m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 32;*/

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            masterMixer.FindSnapshot("TitleMenu").TransitionTo(0.1f);
        }
        else
        {
            playDayMusic();
        }
    }
    public void playDayMusic()
    {
        masterMixer.FindSnapshot("DayMusic").TransitionTo(2f);
    }
    public void playNightMusic()
    {
        masterMixer.FindSnapshot("NightMusic").TransitionTo(2f);
    }

    public void SetSFXLevel(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLevel(float musicLvl)
    {
        masterMixer.SetFloat("musicVol",musicLvl);
    }
    

}