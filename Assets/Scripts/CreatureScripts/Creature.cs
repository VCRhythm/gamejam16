using UnityEngine;

public class Creature : MonoBehaviour, IDamageable {

    public int maxHealth;
    public int health { get; private set; }
    public Color damagedColor;

    public Color color { get { return colors[colorIndex]; } set { meshRenderer.material.color = value; } }

    protected MeshRenderer meshRenderer;
    protected int colorIndex;
    protected bool isShowingDamage = false;
    protected Color[] colors = new Color[3] { Color.blue, Color.red, Color.yellow };

    new private ParticleSystem particleSystem;

    protected virtual void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected virtual void Start()
    {
        health = maxHealth;
        transform.Register();
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
        else
        {
            ShowDamage();
        }
    }

    void ShowDamage()
    {
        if (isShowingDamage) return;

        isShowingDamage = true;
        meshRenderer.material.color = damagedColor;
        if (particleSystem)
        {
            particleSystem.Play();
        }

        Invoke("HideDamage", 1f);
    }

    void HideDamage()
    {
        meshRenderer.material.color = colors[colorIndex];
        isShowingDamage = false;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected Color AssignColor()
    {
        colorIndex = Random.Range(0, colors.Length);
        return colors[colorIndex];
    }

}
