using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    [SerializeField] private GameObject mainDoor;
    [SerializeField] private Transform player;
    [SerializeField] private AudioClip chaseMusic;

    private bool alive=false;
    private bool hunt=false;

    private GameManager GM;
    private Animator anim;
    private UIManager UIM;
    private UnityEngine.AI.NavMeshAgent NMA;
    private AudioSource aud;

    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = gameObject.GetComponent<Animator>();
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        NMA = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive) 
        {
            NMA.SetDestination(player.position);
            if (hunt & NMA.remainingDistance<2.2f) 
            {
                GM.Lose();
                Debug.Log(NMA.remainingDistance);
            }
            //if (hunt) Debug.Log(NMA.remainingDistance);
        }
    }

    public void Chase()
    {
        anim.SetTrigger("Start");
        for (int i=0; i<doors.Length; i++)
        {
            doors[i].GetComponent<DoorController>().Unlock();
            doors[i].GetComponent<DoorController>().Open();
        }
        mainDoor.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
        mainDoor.GetComponent<MeshCollider>().isTrigger = true;
        StartCoroutine(UIM.Subtitle("THE GHOST IS AFTER YOU! Escape through the main entrance!"));
        StartCoroutine(GO());
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(4.5f);
        alive=true;
        aud.clip = chaseMusic;
        aud.Play();
        StartCoroutine(Hunt());
    }
    IEnumerator Hunt()
    {
        yield return new WaitForSeconds(1.5f);
        hunt = true;
    }
}
