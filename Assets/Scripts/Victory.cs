using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    private GameManager GM;

    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GM.Win();
        }
    }
}
