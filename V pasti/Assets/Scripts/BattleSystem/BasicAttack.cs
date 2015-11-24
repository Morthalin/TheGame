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
        if (basePlayer.health > 0 && !basePlayer.pause && !(basePlayer.attacking && !attacking))
        {
            if (timer > 0f)
            {
                if (timer < 0.2f && basePlayer.attacking)
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
                    basePlayer.attacking = true;
                    attacking = true;
                    basePlayer.activeArmor /= 2;
                    hitted = false;
                    timer = 1.5f;
                }
            }
        }
    }

    int DamageCalculation (BasePlayer player, BaseNPC target)
    {
        int damage = (player.strength * 5) + Random.Range(player.minAttack, player.maxAttack);
        int armor = target.armor - (player.agility * 5);

        damage -= armor;
        if (damage <= 0)
            damage = 0;
        return damage;
    }
}
