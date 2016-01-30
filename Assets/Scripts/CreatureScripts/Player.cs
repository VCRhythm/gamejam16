using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInput))]
public class Player : Creature {

    public int foodCount = 0;
    public float colorCycleCooldown = 1f;

    int maxFoodCount = 300;
    float lastColorCycle;

    protected override void Awake()
    {
        base.Awake();
        color = colors[colorIndex];
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            IncreaseFood(other.GetFood().amount);
            Destroy(other.gameObject);
        }
    }

    void IncreaseFood(int amount)
    {
        foodCount += amount;
        foodCount = Mathf.Clamp(foodCount, 0, maxFoodCount);
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
}
