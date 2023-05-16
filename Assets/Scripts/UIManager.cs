using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas UICanvas;
    [SerializeField] private Text subtitle;
    [SerializeField] private Text inventory;
    [SerializeField] private Text hintText;
    [SerializeField] private Text resText;
    [SerializeField] private Text keysTracker;
    [SerializeField] private Text PumpkinTracker;
    [SerializeField] private GameObject[] UIPages;
    [SerializeField] private GameObject resUI;
    [SerializeField] private RawImage crosshair;
    //[SerializeField] private GameObject winScreen;

    

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        keysTracker.text = "Keys Collected: " + GM.keysColleted + "/" + GM.totalKeys;
        PumpkinTracker.text = "Pumpkins Lit: " + GM.pumpkinsLit + "/" + 4;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Sub(string s)
    {
        StartCoroutine(Subtitle(s));
    }

    public void UpdateCrosshair(string clr)
    {
        switch (clr)
        {
            case "red":
            crosshair.color = Color.red;
            break;

            case "green":
            crosshair.color = Color.green;
            break;

            default:
            crosshair.color = Color.white;
            break;
        }
    }

    public void OpenInventory()
    {
        string inv = "";
        foreach (string s in GM.inventory)
        {
            inv += s;
            inv += "\n";
        }
        inventory.text = inv;
        UIPages[0].SetActive(true);
    }

    public void CloseInventory()
    {
        UIPages[0].SetActive(false);
    }

    public void CloseAll()
    {
        foreach (var x in UIPages)
        {
            x.SetActive(false);
        }
    }

    public void OpenHint(string s)
    {
        hintText.text = s;
        UIPages[1].SetActive(true);
    }

    public void showResult(string s)
    {
        resText.text=s;
        resUI.SetActive(true);
    }

    public void Win(float t)
    {
        double mins = Math.Floor(t / 60);
        double sec = Math.Floor(t % 60);
        showResult("You Successfully Escaped!!!\nTime taken = " + mins + " mins and " + sec + " seconds.");
    }

    public void Lose()
    {
        showResult("You were caught! Better luck next time!");
    }

    public void UpdateKeyTracker()
    {
        keysTracker.text = "Keys Collected: " + GM.keysColleted + "/" + GM.totalKeys;
    }

    public void UpdatePTracker()
    {
        PumpkinTracker.text = "Pumpkins Lit: " + GM.pumpkinsLit + "/" + GM.totalPumps;
    }

    public IEnumerator Subtitle(string sub)
    {
        subtitle.text = sub;
        yield return new WaitForSeconds(1.5f);
        subtitle.text = "";
    }
}
