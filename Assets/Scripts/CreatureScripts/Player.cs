using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInput))]
public class Player : Creature {

    public override Color color
    {
        get { return colors[colorIndex]; }
        set
        {
            //meshRenderer.material.color = value;
        }
    }

    public int cheese = 0;
    public float colorCycleCooldown = 1f;

    int maxFoodCount = 300;
    float lastColorCycle;
    TowerConstruction towerConstruction;
    PlayerInput input;
    Transform model;

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
        color = colors[colorIndex];
        model = transform.GetChild(0);
    }

    protected override void Start()
    {
        base.Start();

        towerConstruction = FindObjectOfType<TowerConstruction>();
    }

    void Update()
    {
        if(input.isTogglingBuildMenu)
        {
            towerConstruction.ToggleBuildMenu();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Food"))
        {
            Debug.Log(other.transform);
            IncreaseFood(other.transform.GetFood().amount);
            Destroy(other.gameObject);
        }
    }

    void IncreaseFood(int amount)
    {
        cheese += amount;
        cheese = Mathf.Clamp(cheese, 0, maxFoodCount);
    }

    public void GetTower(Tower tower)
    {
        cheese -= tower.cost;
        Instantiate(tower, transform.position + model.forward * 2f, Quaternion.identity);
    }

    public void CycleColor(bool cycleForwards)
    {
        if (Time.time - lastColorCycle < colorCycleCooldown) return;
        lastColorCycle = Time.time;

        colorIndex += cycleForwards ? 1 : -1;
        if(colorIndex >= colors.Length)
        {
            colorIndex = 0;
        }
        else if(colorIndex < 0)
        {
            colorIndex = colors.Length - 1;
        }

        if (!isShowingDamage)
        {
            color = colors[colorIndex];
        }
    }

    protected override void Die()
    {
        enabled = false;
    }

}
