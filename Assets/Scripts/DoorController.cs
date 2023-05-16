using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool locked = true;
    public bool open = false;
    
    private UIManager UIM;
    private Animator doorAnim;
    private AudioSource audio;

    public void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
        audio = gameObject.GetComponent<AudioSource>();
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        
    }

    public void Unlock()
    {
        locked=false;
    }

    public void Open()
    {   
        if (locked) StartCoroutine(UIM.Subtitle("This door is locked."));
        else if (!open)
        {
            open = true;
            doorAnim.SetTrigger("Open");
            audio.Play();
        }
    }
}
