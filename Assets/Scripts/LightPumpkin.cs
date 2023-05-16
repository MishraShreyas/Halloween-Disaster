using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPumpkin : MonoBehaviour
{
    [SerializeField] private GameObject litPumpkin;
    [SerializeField] private AudioClip tra;

    private GameManager GM;
    private UIManager UIM;
    private AudioSource aso;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        aso = GetComponent<AudioSource>();
    }

    public void Light()
    {
        GM.pumpkinsLit++;
        UIM.UpdatePTracker();
        if (!GM.inventory.Contains("Lighter: A Lighter to light up pumpkins!"))
        {
            StartCoroutine(UIM.Subtitle("You do not have a lighter."));
            return;
        }
        aso.clip = tra;
        aso.Play();
        litPumpkin.SetActive(true);
        gameObject.SetActive(false);
    }
}
