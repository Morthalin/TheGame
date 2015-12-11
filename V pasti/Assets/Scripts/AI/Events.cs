using UnityEngine;
using System.Collections;

public class Events : MonoBehaviour
{
    int ticks = 0;
    bool casting = false;
    public GameObject fireball;

    public void CastAttack()
    {
        if (!casting)
        {
            casting = true;

            Animator animator = transform.GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("Missing mage Animator!");
            }
            Transform player = GameObject.Find("Player").transform;
            if (!player)
            {
                Debug.LogError("Missing Player!");
            }

            if ((player.position - transform.position).sqrMagnitude > 240)
            {
                animator.SetTrigger("castFireball");
                StartCoroutine(Fireball());
            }
            else
            {
                if (Random.Range(0, 100) > 66)
                {
                    StartCoroutine(Blink());
                }
                else
                {
                    StartCoroutine(Burp());
                }
            }
        }
    }

    public void FireballTick()
    {
        if (ticks != 0)
        {
            Transform player = GameObject.Find("Player").transform;
            if (!player)
            {
                Debug.LogError("Missing Player!");
            }

            Animator playerAnimator = player.GetComponent<Animator>();
            if (!playerAnimator)
            {
                Debug.LogError("Missing player Animator!");
            }

            playerAnimator.SetTrigger("damage");
            player.GetComponent<BasePlayer>().health -= 20;
            ticks--;
        }
    }

    public void CastBlink()
    {
        if (!casting)
        {
            casting = true;
            StartCoroutine(Blink());
        }
    }
    

    //Casove funkcie


    IEnumerator Fireball()
    {
        GameObject localFireball;
        Vector3 movementVector;
        Transform player = GameObject.Find("Player").transform;
        if (!player)
        {
            Debug.LogError("Missing Player!");
        }
        Transform initFireball = transform.FindChild("InitFireball");
        if (!initFireball)
        {
            Debug.LogError("Missing Fireball prefab!");
        }
        Animator playerAnimator = player.GetComponent<Animator>();
        if (!playerAnimator)
        {
            Debug.LogError("Missing player Animator!");
        }

        yield return new WaitForSeconds(2);
        localFireball = (GameObject)Instantiate(fireball, initFireball.position, initFireball.rotation);
        while ((localFireball.transform.position - player.position).sqrMagnitude > 5f)
        {
            localFireball.transform.rotation = Quaternion.Lerp(localFireball.transform.rotation, Quaternion.LookRotation(player.position - localFireball.transform.position), 1f);
            movementVector = localFireball.transform.TransformDirection(0f, 0f, 2f);
            localFireball.transform.position += movementVector;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(localFireball);
        playerAnimator.SetTrigger("damage");
        player.GetComponent<BasePlayer>().health -= 200;
        ticks = 10;
        casting = false;
    }

    IEnumerator Blink()
    {
        Transform[] blinkTargets = GameObject.Find("BlinkTargets").GetComponentsInChildren<Transform>();
        Transform player = GameObject.Find("Player").transform;
        if (!player)
        {
            Debug.LogError("Missing Player!");
        }
        Animator animator = transform.GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Missing Animator!");
        }

        if ((transform.position - player.position).sqrMagnitude < 100)
        {
            animator.SetTrigger("castBlink");
            transform.FindChild("BlinkStart").GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(2);
            transform.FindChild("BlinkEnd").GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(0.3f);
            transform.position = blinkTargets[Random.Range(0, blinkTargets.Length - 1)].position;
        }
        casting = false;
    }

    IEnumerator Burp()
    {
        Transform player = GameObject.Find("Player").transform;
        if (!player)
        {
            Debug.LogError("Missing Player!");
        }
        Animator animator = transform.GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("Missing Animator!");
        }
        Animator playerAnimator = player.GetComponent<Animator>();
        if (!playerAnimator)
        {
            Debug.LogError("Missing player Animator!");
        }
        Transform camera = player.FindChild("Camera Target").FindChild("Main Camera");
        if (!camera)
        {
            Debug.LogError("Missing Main Camera!");
        }

        transform.FindChild("BurpEffect").GetComponent<ParticleSystem>().Play();
        camera.GetComponent<CameraShaking>().shaking = true;
        for (int i = 0; i < 10; i++)
        {
            if((player.position - transform.position).sqrMagnitude < 240)
                player.GetComponent<BasePlayer>().health -= 20;
            yield return new WaitForSeconds(0.5f);
        }
        camera.GetComponent<CameraShaking>().shaking = false;
        transform.FindChild("BurpEffect").GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(2f);
        casting = false;
    }
}
