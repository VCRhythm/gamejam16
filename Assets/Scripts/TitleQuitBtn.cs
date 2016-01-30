using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TitleQuitBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnClicked()
    {
        Debug.Log("we would quit here2");
        Application.Quit();
    }
    
}
