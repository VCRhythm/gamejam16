using UnityEngine;
using UnityEngine.UI;

public class TowerConstruction : MonoBehaviour {

    public Tower[] towers;
    public CanvasGroup towerCanvas;
    public Button buttonPrefab;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        MakeButtons();
    }

    public void BuyTower(int index)
    {
        Debug.Log(index);
        if(player.cheese >= towers[index].cost)
        {
            player.GetTower(towers[index]);
        }
    }

    public void ToggleBuildMenu()
    {
        ShowCanvas(towerCanvas.alpha == 1 ? false : true);
    }

    void ShowCanvas(bool isTrue)
    {
        towerCanvas.alpha = isTrue ? 1 : 0;
        towerCanvas.interactable = isTrue;
        towerCanvas.blocksRaycasts = isTrue;
    }

    void MakeButtons()
    {
        RectTransform canvasPanel = towerCanvas.transform.GetChild(0).GetComponent<RectTransform>();
        for(int i=0; i < towers.Length; i++)
        {
            Button button = Instantiate(buttonPrefab) as Button;
            Text[] texts = button.GetComponentsInChildren<Text>();
            texts[0].text = towers[i].name;
            texts[1].text = towers[i].cost.ToString();
            int index = i;
            button.onClick.AddListener(() => { BuyTower(index); });
            Debug.Log(i);

            button.transform.SetParent(canvasPanel);
        }
    }    
}
