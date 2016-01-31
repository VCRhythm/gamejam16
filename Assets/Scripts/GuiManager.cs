using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {
    private Player player;
    private Homebase homeBase;
    private LevelManager levelManager;
    private CanvasGroup gameOverPanel;

    GameObject[] GUIItems;

    void Start()
    {
        gameOverPanel = transform.FindChild("GameOverPanel").GetComponent<CanvasGroup>();
        player = FindObjectOfType<Player>();
        homeBase = FindObjectOfType<Homebase>();
        levelManager = FindObjectOfType<LevelManager>();

        GUIItems = GameObject.FindGameObjectsWithTag("GUIItem");
    }

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

    void updatePlayerHP(GameObject GUIItem)
    {
        int health = player.health;
        int maxhealth = player.maxHealth;
        GUIItem.transform.FindChild("PlayerHPText").GetComponent<Text>().text = health + " / " + maxhealth;
        GUIItem.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Floor(((float)health / (float)maxhealth) * 460), GUIItem.GetComponent<RectTransform>().rect.height);

        if(health <= 0 )
        {
            gameOverPanel.alpha = 1;
            gameOverPanel.blocksRaycasts = true;
            gameOverPanel.interactable = true;
        }
    }

    void updateBaseHP(GameObject GUIItem)
    {
        int health = homeBase.health;
        int maxhealth = homeBase.maxHealth;
        GUIItem.transform.FindChild("BaseHPText").GetComponent<Text>().text = health + " / " + maxhealth;
        GUIItem.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Floor(((float)health / (float)maxhealth) * 460), GUIItem.GetComponent<RectTransform>().rect.height);

        if (health <= 0)
        {
            gameOverPanel.alpha = 1;
            gameOverPanel.blocksRaycasts = true;
            gameOverPanel.interactable = true;
        }
    }

    void updateCheeseText(GameObject GUIItem)
    {
        GUIItem.GetComponent<Text>().text = "Cheese: " + player.cheese + " / " + player.maxCheese;
    }

    void updateWaveNum(GameObject GUIItem)
    {
        GUIItem.GetComponent<Text>().text = levelManager.night.ToString();
    }

    void updateDayStateText(GameObject GUIItem)
    {
        GUIItem.GetComponent<Text>().text = levelManager.isDay ? "Day" : " Night";
    }

    void updateEnemiesLeftText(GameObject GUIItem)
    {
        if (levelManager.isDay)
        {
            int hour = Mathf.FloorToInt(levelManager.dayTimeLeft / 60);
            GUIItem.GetComponent<Text>().text = hour > 0 ? (Mathf.FloorToInt(levelManager.dayTimeLeft / 60) + ":") : "" + Mathf.FloorToInt(levelManager.dayTimeLeft % 60);
        }
        else
        {
            GUIItem.GetComponent<Text>().text = levelManager.enemiesRemaining + "/" + levelManager.enemiesStarted;
        }
    }

}
