using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerActions : CreatureActions {

    PlayerInput input;
    Player player;
    public AudioClip attackSound;
    private float volLowRange = .5f;
    private float volHighRange = 1f;
    private AudioSource source;
    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
        input = GetComponent<PlayerInput>();
        source = GetComponent<AudioSource>();
        attackLayer = enemyLayer;
    }

    void FixedUpdate()
    {
        if(input.isAttacking)
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(attackSound, vol);
            Attack();
        }

        if (input.colorCycle != 0)
        {
            player.CycleColor(input.colorCycle > 0);
        }
    }

}
