using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {
    public Player player;
    public Homebase homeBase;
    //TODO: wave manager
    //TODO: object with Day/Night

    GameObject[] GUIItems;

    void Start()
    {
        GUIItems = GameObject.FindGameObjectsWithTag("GUIItem");
    }

	// Update is called once per frame
	void Update () {
        foreach (GameObject GUIItem in GUIItems) {
            if (GUIItem.name=="BaseHP")
            {
                updateBaseHP(GUIItem);
            }
            if (GUIItem.name == "PlayerHP")
            {
                updatePlayerHP(GUIItem);
            }
            if (GUIItem.name == "CheeseText")
            {
                updateCheeseText(GUIItem);
            }
            if (GUIItem.name == "WaveNum")
            {
                updateWaveNum(GUIItem);
            }
            if (GUIItem.name == "GameDayStateText")
            {
                updateDayStateText(GUIItem);
            }
            if (GUIItem.name == "EnemiesLeftText")
            {
                updateEnemiesLeftText(GUIItem);
            }
        }
    }

    void updatePlayerHP(GameObject GUIItem) {
        int health = player.health;
        int maxhealth = 300;//TO DO: max health property from player
        GUIItem.transform.FindChild("PlayerHPText").GetComponent<Text>().text = health + " / " + maxhealth;
        GUIItem.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Floor(((float)health / (float)maxhealth) * 460), GUIItem.GetComponent<RectTransform>().rect.height);
    }

    void updateBaseHP(GameObject GUIItem)
    {
        int health = homeBase.health;
        int maxhealth = 700;//TO DO: max health property from homebase
        GUIItem.transform.FindChild("BaseHPText").GetComponent<Text>().text = health + " / " + maxhealth;
        GUIItem.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Floor(((float)health / (float)maxhealth) * 460), GUIItem.GetComponent<RectTransform>().rect.height);
    }

    void updateCheeseText(GameObject GUIItem)
    {
        int foodCount = player.cheese;
        int maxcheese = 300;
        Debug.Log(GUIItem);
        GUIItem.GetComponent<Text>().text = "Cheese: " + foodCount + " / " + maxcheese;
    }

    void updateWaveNum(GameObject GUIItem)
    {
        int wavenum = 2;//TO DO: get from Wave manager
        GUIItem.GetComponent<Text>().text = ""+ wavenum;
    }

    void updateDayStateText(GameObject GUIItem)
    {
        //TO DO: get value from somewhere
        GUIItem.GetComponent<Text>().text = "Day";
    }

    void updateEnemiesLeftText(GameObject GUIItem)
    {
        //TO DO: get from wave manager
        GUIItem.GetComponent<Text>().text = "13 / 30";
    }

}
