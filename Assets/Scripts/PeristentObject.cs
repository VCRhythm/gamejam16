using UnityEngine;
using System.Collections;

public class PeristentObject : MonoBehaviour
{

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static PeristentObject s_Instance = null;

    public int graphicsVal = 0;//0-5
    public float musicVal = -20;//-80:-10 decibels
    public float sfxVal = -10;//-80:0 decibels

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static PeristentObject instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(PeristentObject)) as PeristentObject;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("PeristentObject");
                s_Instance = obj.AddComponent(typeof(PeristentObject)) as PeristentObject;
                Debug.Log("Could not locate an PeristentObject object. PeristentObject was Generated Automaticly.");
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }

    // Add the rest of the code here...
    public void DoSomeThing()
    {
        Debug.Log("Doing something now", this);
    }
}
