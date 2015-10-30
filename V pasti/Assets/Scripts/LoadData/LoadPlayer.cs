using UnityEngine;
using System.Collections;

public class LoadPlayer : MonoBehaviour
{
    private BasePlayer player;

    void Awake()
    {
        player = new BasePlayer("Morth");
    }
}
