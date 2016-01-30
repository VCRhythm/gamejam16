using UnityEngine;

public class Creature : MonoBehaviour {

    public int health = 3;
    public Material damagedMaterial;

    MeshRenderer meshRenderer;
    Material originalMaterial;
    bool isShowingDamage = false;

    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    void Start()
    {
        TransformExtensions.Register(transform, GetComponent<Collider>());
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
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
        meshRenderer.material = damagedMaterial;

        Invoke("HideDamage", 1f);
    }

    void HideDamage()
    {
        meshRenderer.material = originalMaterial;
        isShowingDamage = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
