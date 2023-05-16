using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public List<GameObject> doors;
    public string desc;
    [SerializeField] private string itemName;
    private UIManager UIM;
    private GameManager GM;

    public void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Collect()
    {
        if (gameObject.name != "Lighter") GM.keysColleted++;
        UIM.UpdateKeyTracker();
        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorController>().Unlock();
        }
        string inv = itemName + ": " + desc;
        GM.inventory.Add(inv);
        UIM.Sub("You have found a " + itemName);
        Destroy(gameObject);
    }

}
