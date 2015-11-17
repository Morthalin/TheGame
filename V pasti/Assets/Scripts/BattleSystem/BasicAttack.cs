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
        hitted = false;
	}
	
	void Update ()
    {
        if (basePlayer.health > 0)
        {
            RaycastHit hit;

            if (timer > 0f)
            {
                if (timer < 0.5f)
                    attacking = false;
                timer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    animator.SetTrigger("Attack1");
                    attacking = true;
                    hitted = false;
                    timer = 1.5f;
                }
            }
            int layerMask = 1 << 9;
            if (Physics.Linecast(transform.position, transform.parent.position, out hit, layerMask))
            {
                if (!hitted && attacking)
                {
                    hit.transform.gameObject.GetComponent<BaseNPC>().health -= DamageCalculation(GameObject.Find("Player").GetComponent<BasePlayer>(), hit.transform.gameObject.GetComponent<BaseNPC>());
                    hit.transform.gameObject.GetComponent<Animator>().SetTrigger("damage");
                    HPBarChange(hit.transform.gameObject);
                    hitted = true;
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

    void HPBarChange(GameObject target)
    {
        float percentage = (float)target.GetComponent<BaseNPC>().health / (float)target.GetComponent<BaseNPC>().healthMax;

        target.transform.Find("HPFrame").transform.Find("HPBar").GetComponent<Image>().fillAmount = percentage;
        target.transform.Find("HPFrame").transform.Find("HPBar").transform.Find("Text").GetComponent<Text>().text = target.GetComponent<BaseNPC>().health.ToString() + "/" + target.GetComponent<BaseNPC>().healthMax.ToString();
    }
}
