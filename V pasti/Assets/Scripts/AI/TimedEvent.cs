using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class TimedEvent : MonoBehaviour
{
    public string methodName; //Meno metody na spustenie
    public bool repeatable; //Opakovatelny event
    public bool combatTiming; //Pocitanie v combate
    public float firstMin; //Cas spustenia v sekundach
    public float firstMax; //Cas spustenia v sekundach
    public float repeatMin; //Interval opakovania v sekundach
    public float repeatMax; //Interval opakovania v sekundach
    private float timer; //Casovac
    private MethodInfo method; //Volana metoda
    private Events events; //Trieda metod

    void Awake ()
    {
        //Inicializacia metody
        Type type = typeof(Events);
        method = type.GetMethod(methodName);
        if(method == null)
        {
            Debug.LogError("Metoda \"" + methodName + "\" neexistuje!");
        }
        events = transform.GetComponent<Events>();
	}
	
	void Update ()
    {
        if(combatTiming && timer != -1)
        {
            //V kombate
            if(transform.GetComponent<BaseNPC>().inCombat)
            {
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                    if (timer < 0)
                        timer = 0;
                }
                else
                {
                    if(repeatable)
                    {
                        method.Invoke(events, null); //Spustenie eventu
                        timer = UnityEngine.Random.Range(repeatMin, repeatMax);
                    }
                    else
                    {
                        method.Invoke(events, null); //Spustenie eventu
                        timer = -1; //Blokonavie opakovania
                    }
                }
            }
        }
        else if(timer != -1)
        {
            //Mimo kombatu
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                    timer = 0;
            }
            else
            {
                if (repeatable)
                {
                    method.Invoke(events, null); //Spustenie eventu
                    timer = UnityEngine.Random.Range(repeatMin, repeatMax);
                }
                else
                {
                    method.Invoke(events, null); //Spustenie eventu
                    timer = -1; //Blokonavie opakovania
                }
            }
        }
	}
}
