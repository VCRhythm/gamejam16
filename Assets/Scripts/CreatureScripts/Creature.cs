using UnityEngine;

public class Creature : MonoBehaviour, IDamageable {

    public int maxHealth;
    public int health { get; protected set; }
    public Color damagedColor;

    public virtual Color color { get { return colors[colorIndex]; } set {
            meshRenderer.material.color = value;
        } }

    protected SkinnedMeshRenderer meshRenderer;
    protected int colorIndex;
    protected bool isShowingDamage = false;
    protected Color[] colors = new Color[3] { Color.blue, Color.red, Color.yellow };

    new private ParticleSystem particleSystem;
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
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
        
        if (particleSystem)
        {
            particleSystem.Play();
        }

        animator.SetTrigger("Damaged");
    }

    void HideDamage()
    {
        isShowingDamage = false;
    }

    protected virtual void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject);
    }

    protected Color AssignColor()
    {
        colorIndex = Random.Range(0, colors.Length);
        return colors[colorIndex];
    }

}
