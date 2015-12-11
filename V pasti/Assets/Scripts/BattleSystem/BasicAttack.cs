using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BasicAttack : MonoBehaviour
{
    private Animator animator;
    private float timer;
    private BasePlayer basePlayer;
    private bool hitted;
    private bool attacking;
    private AudioSource audioSource;
    public AudioClip[] clip;

    void Start ()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        if(!animator)
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
        hitted = true;
        attacking = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 9)
        {
            if (!hitted && basePlayer.attacking)
            {
                collider.gameObject.GetComponent<BaseNPC>().health -= DamageCalculation(GameObject.Find("Player").GetComponent<BasePlayer>(), collider.gameObject.GetComponent<BaseNPC>());
                collider.gameObject.GetComponent<Animator>().SetTrigger("damage");
                //HPBarChange(hit.transform.gameObject);
                hitted = true;
            }
        }
    }

    void Update ()
    {
        if (basePlayer.health > 0 && basePlayer.pause == 0 && !(basePlayer.attacking && !attacking))
        {
            if (timer > 0f)
            {
                if (timer < 0.5f && basePlayer.attacking)
                {
                    basePlayer.attacking = false;
                    attacking = false;
                    basePlayer.activeArmor = basePlayer.armor;
                }
                timer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    animator.SetTrigger("Attack1");
                    audioSource.clip = clip[Random.Range(0, clip.Length)];
                    audioSource.volume = Random.Range(0.5f, 1f);
                    audioSource.pitch = 0.7f;
                    audioSource.PlayDelayed(0.5f);

                    basePlayer.attacking = true;
                    attacking = true;
                    basePlayer.activeArmor /= 2;
                    hitted = false;
                    timer = 2f;
                }
            }
        }
    }

    int DamageCalculation (BasePlayer player, BaseNPC target)
    {
        int damage = ((player.strength ^ 3  / 32) + 32) * Random.Range(player.minAttack, player.maxAttack) / 16;
        int defense = (((target.armor - 280) ^ 2) / 110) + 16;
        int baseDamage = damage * defense / 730;
        int finalDamage = baseDamage * (730 - (defense * 51 - defense ^ 2 / 11) / 10) / 730;
        
        if (finalDamage <= 0)
            finalDamage = 0;
        return finalDamage;
    }
}
