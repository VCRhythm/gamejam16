using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour {
    GameObject PersistenceGameObject;
    CanvasGroup canvasGroup;
	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectsWithTag("PersistentObject").Length>0)
        {
            PersistenceGameObject = GameObject.FindGameObjectWithTag("PersistentObject");
        }
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Trigger()
    {
        canvasGroup.alpha = 1;
        Time.timeScale = 0;
    }

    public void ResetLevel() {
        Debug.Log("restarting level");
        DontDestroyOnLoad(PersistenceGameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void returnToMainMenu()
    {
        Debug.Log("returning to main menu");
        DontDestroyOnLoad(PersistenceGameObject);
        SceneManager.LoadScene(0);
    }
}
