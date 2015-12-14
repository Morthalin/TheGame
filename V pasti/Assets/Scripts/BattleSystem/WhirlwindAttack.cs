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
    public float cooldown = 10f;
    public Button cooldownIndicator;
    public int energy = 30;

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

        if (!cooldownIndicator)
        {
            Debug.LogError("Missing cooldown button!");
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
                int damage = DamageCalculation(GameObject.Find("Player").GetComponent<BasePlayer>(), collider.transform.gameObject.GetComponent<BaseNPC>());
                collider.gameObject.GetComponent<BaseNPC>().health -= damage;
                collider.gameObject.GetComponent<Animator>().SetTrigger("damage");
                collider.transform.FindChild("FloatingDamageText").FindChild("Text").GetComponent<Text>().text = damage.ToString();
                collider.transform.FindChild("FloatingDamageText").FindChild("Text").GetComponent<Animator>().SetTrigger("Hit");
                collider.gameObject.GetComponent<BaseNPC>().hitted = true;
                targets.Add(collider.gameObject.GetComponent<BaseNPC>());
            }
        }
    }

    void Update ()
    {
        if (basePlayer.health > 0)
        {
            if (timer > 0f)
            {
                cooldownIndicator.GetComponent<Image>().fillAmount = timer / cooldown;
                if (timer < cooldown - 2f && attacking)
                {
                    basePlayer.attacking = false;
                    attacking = false;
                    basePlayer.activeArmor = basePlayer.armor;
                }
                timer -= Time.deltaTime;
            }
            else
            {
                cooldownIndicator.GetComponent<Image>().fillAmount = 0;
                if (Input.GetMouseButton(1) && basePlayer.energy >= energy && basePlayer.pause == 0 && !basePlayer.attacking && !attacking)
                {
                    basePlayer.energy -= energy;
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
                    timer = cooldown;
                    cooldownIndicator.GetComponent<Image>().fillAmount = 1;
                }
            }
        }
    }

    int DamageCalculation(BasePlayer player, BaseNPC target)
    {
        int damage = ((player.strength ^ 3 / 32) + 32) * Random.Range(player.minAttack, player.maxAttack) / 16;
        int defense = (((target.armor - 280) ^ 2) / 110) + 16;
        int baseDamage = damage * defense / 730;
        int finalDamage = baseDamage * (730 - (defense * 51 - defense ^ 2 / 11) / 10) / 730;

        if (finalDamage <= 0)
            finalDamage = 0;
        return finalDamage * 2;
    }

    void ResetHitted (List<BaseNPC> targets)
    {
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].hitted = false;
        }
    }
}
