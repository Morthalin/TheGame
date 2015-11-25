using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WhirlwindAttack : MonoBehaviour
{
    private Animator animator;
    private float timer;
    private BasePlayer basePlayer;
    private List<BaseNPC> targets;
    private bool attacking;
    private AudioSource audioSource;
    public AudioClip[] clip;

    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Missing animator!");
        }

        basePlayer = GameObject.Find("Player").GetComponent<BasePlayer>();
        if (!basePlayer)
        {
            Debug.LogError("Missing BasePlayer script!");
        }

        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        if (!audioSource)
        {
            Debug.LogError("Missing audio source!");
        }

        if (clip.Length == 0)
        {
            Debug.LogError("0 sounds for attacking!");
        }

        timer = 0f;
        targets = new List<BaseNPC>();
        attacking = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 9)
        {
            if (!collider.gameObject.GetComponent<BaseNPC>().hitted && basePlayer.attacking)
            {
                collider.gameObject.GetComponent<BaseNPC>().health -= DamageCalculation(GameObject.Find("Player").GetComponent<BasePlayer>(), collider.transform.gameObject.GetComponent<BaseNPC>());
                collider.gameObject.GetComponent<Animator>().SetTrigger("damage");
                collider.gameObject.GetComponent<BaseNPC>().hitted = true;
                targets.Add(collider.gameObject.GetComponent<BaseNPC>());
            }
        }
    }

    void Update ()
    {
        if (basePlayer.health > 0 && !basePlayer.pause && !(basePlayer.attacking && !attacking))
        {
            if (timer > 0f)
            {
                if (timer < 3f && basePlayer.attacking)
                {
                    basePlayer.attacking = false;
                    attacking = false;
                    basePlayer.activeArmor = basePlayer.armor;
                }
                timer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(1))
                {
                    audioSource.clip = clip[Random.Range(0, clip.Length)];
                    audioSource.volume = Random.Range(0.5f, 1f);
                    audioSource.pitch = 1f;
                    audioSource.PlayDelayed(0.7f);
                    animator.SetTrigger("Attack2");
                    basePlayer.attacking = true;
                    attacking = true;
                    basePlayer.activeArmor /= 2;
                    ResetHitted(targets);
                    targets.Clear();
                    timer = 4f;
                }
            }
        }
    }

    int DamageCalculation(BasePlayer player, BaseNPC target)
    {
        int damage = (player.strength * 5) + Random.Range(player.minAttack, player.maxAttack) * 2;
        int armor = target.armor - (player.agility * 5);

        damage -= armor;
        if (damage <= 0)
            damage = 0;
        return damage;
    }

    void ResetHitted (List<BaseNPC> targets)
    {
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].hitted = false;
        }
    }
}
