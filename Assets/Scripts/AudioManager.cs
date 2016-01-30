using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioMixerSnapshot TitleMenuMusic;
    public AudioMixerSnapshot inCombat;
    public AudioClip[] stings;
    public AudioSource stingSource;
    public float bpm = 128;

    public static float globalMusicVolume = .5f;

    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;

    public AudioMixer masterMixer;

    // Use this for initialization
    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 32;
        if (Application.loadedLevel == 0) {
        }
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