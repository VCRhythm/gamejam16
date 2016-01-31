using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour {
    GameObject PersistenceGameObject;
	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectsWithTag("PersistentObject").Length>0)
        {
            PersistenceGameObject = GameObject.FindGameObjectWithTag("PersistentObject");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResetLevel() {
        DontDestroyOnLoad(PersistenceGameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void returnToMainMenu()
    {
        DontDestroyOnLoad(PersistenceGameObject);
        SceneManager.LoadScene(0);
    }
}
