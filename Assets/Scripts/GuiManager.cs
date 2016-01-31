using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {
    private Player player;
    private Homebase homeBase;
    private LevelManager levelManager;
    //TODO: wave manager
    //TODO: object with Day/Night

    GameObject[] GUIItems;

    void Start()
    {
        player = FindObjectOfType<Player>();
        homeBase = FindObjectOfType<Homebase>();
        levelManager = FindObjectOfType<LevelManager>();

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
        int maxhealth = player.maxHealth;
        GUIItem.transform.FindChild("PlayerHPText").GetComponent<Text>().text = health + " / " + maxhealth;
        GUIItem.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Floor(((float)health / (float)maxhealth) * 460), GUIItem.GetComponent<RectTransform>().rect.height);
    }

    void updateBaseHP(GameObject GUIItem)
    {
        int health = homeBase.health;
        int maxhealth = homeBase.maxHealth;
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
        GUIItem.GetComponent<Text>().text = levelManager.night.ToString();
    }

    void updateDayStateText(GameObject GUIItem)
    {
        //TO DO: get value from somewhere
        GUIItem.GetComponent<Text>().text = levelManager.isDay ? "Day" : " Night";
    }

    void updateEnemiesLeftText(GameObject GUIItem)
    {
        //TO DO: get from wave manager
        GUIItem.GetComponent<Text>().text = levelManager.enemiesRemaining + "/" + levelManager.enemiesStarted;
    }

}
