using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WhirlwindAttack : MonoBehaviour
{
    private Animator animator;
    private float timer;
    private BasePlayer basePlayer;
    private List<BaseNPC> targets;

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

        timer = 0f;
        targets = new List<BaseNPC>();
}

void Update ()
    {
        if (basePlayer.health > 0)
        {
            RaycastHit hit;

            if (timer > 0f)
            {
                if (timer < 3f)
                {
                    basePlayer.attacking = false;
                    basePlayer.activeArmor = basePlayer.armor;
                }
                timer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(1))
                {
                    animator.SetTrigger("Attack2");
                    basePlayer.attacking = true;
                    basePlayer.activeArmor /= 2;
                    ResetHitted(targets);
                    targets.Clear();
                    timer = 4f;
                }
            }
            int layerMask = 1 << 9;
            if (Physics.Linecast(transform.position, transform.parent.parent.parent.parent.parent.parent.parent.position, out hit, layerMask))
            {
                if (!hit.transform.gameObject.GetComponent<BaseNPC>().hitted && basePlayer.attacking)
                {
                    hit.transform.gameObject.GetComponent<BaseNPC>().health -= DamageCalculation(GameObject.Find("Player").GetComponent<BasePlayer>(), hit.transform.gameObject.GetComponent<BaseNPC>());
                    hit.transform.gameObject.GetComponent<Animator>().SetTrigger("damage");
                    //HPBarChange(hit.transform.gameObject);
                    hit.transform.gameObject.GetComponent<BaseNPC>().hitted = true;
                    targets.Add(hit.transform.gameObject.GetComponent<BaseNPC>());
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

    //void HPBarChange(GameObject target)
    //{
    //    float percentage = (float)target.GetComponent<BaseNPC>().health / (float)target.GetComponent<BaseNPC>().healthMax;

    //    target.transform.Find("HPFrame").transform.Find("HPBar").GetComponent<Image>().fillAmount = percentage;
    //    target.transform.Find("HPFrame").transform.Find("HPBar").transform.Find("Text").GetComponent<Text>().text = target.GetComponent<BaseNPC>().health.ToString() + "/" + target.GetComponent<BaseNPC>().healthMax.ToString();
    //}

    void ResetHitted (List<BaseNPC> targets)
    {
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].hitted = false;
        }
    }
}
