﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillController : MonoBehaviour
{
    public Transform target;
    private BaseNPC baseNPC;
    private BasePlayer basePlayer;
    private bool player;
    public bool energy = false;

	void Start ()
    {
        baseNPC = target.GetComponent<BaseNPC>();
	    if(!baseNPC)
        {
            basePlayer = target.GetComponent<BasePlayer>();
            if(!basePlayer)
            {
                Debug.LogError("Missing or wrong target!");
            }
            else
            {
                player = true;
            }
        }
        else
        {
            player = false;
        }
	}
	
	void Update ()
    {
        if (!energy)
        {
            if (player)
            {
                if (basePlayer.health <= 0)
                {
                    basePlayer.health = 0;
                }
                else
                {
                    basePlayer.dead = false;
                }
                float percentage = (float)basePlayer.health / (float)basePlayer.healthMax;

                transform.GetComponent<Image>().fillAmount = percentage;
                transform.Find("Text").GetComponent<Text>().text = basePlayer.health.ToString() + "/" + basePlayer.healthMax.ToString();
            }
            else
            {
                float percentage = (float)baseNPC.health / (float)baseNPC.healthMax;

                transform.GetComponent<Image>().fillAmount = percentage;
                transform.Find("Text").GetComponent<Text>().text = baseNPC.health.ToString() + "/" + baseNPC.healthMax.ToString();
            }
        }
        else
        {
            if (basePlayer.energy < 0)
                basePlayer.energy = 0;
            float percentage = (float)basePlayer.energy / (float)basePlayer.energyMax;

            transform.GetComponent<Image>().fillAmount = percentage;
            transform.Find("Text").GetComponent<Text>().text = basePlayer.energy.ToString() + "/" + basePlayer.energyMax.ToString();
        }
	}
}
