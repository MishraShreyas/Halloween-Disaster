using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pumpkins;
    [SerializeField] private List<GameObject> clueRooms;
    [SerializeField] private GameObject[] pumps;
    [SerializeField] private GameObject[] clues;
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject firstKey;
    [SerializeField] private string[] lore;

    public List<string> inventory;
    public int pumpkinsLit;
    public bool paused;
    public int keysColleted=0;
    public int totalKeys;
    public int totalPumps;
    public int rooms;

    private UIManager UIM;
    private bool gameEnded=false;
    private float timer=0f;

    // Start is called before the first frame update
    void Start()
    {
        pumpkinsLit = 0;
        paused=false;
        inventory = new List<string>();
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();

        PrepareRooms();

        UIM.UpdateKeyTracker();
        UIM.UpdatePTracker();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPumpkins())
        {
            pumpkinsLit = 0;
            ghost.SetActive(true);
            ghost.GetComponent<GhostChase>().Chase();
        }

        if (!gameEnded)
        {
            timer += Time.deltaTime;
        }
    }

    private bool CheckPumpkins()
    {
        return pumpkinsLit == pumpkins.Count;
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        if (gameEnded) return;
        paused = false;
        Time.timeScale = 1;
    }

    public void Win()
    {
        gameEnded=true;
        UIM.Win(timer);
        Pause();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Lose()
    {
        UIM.Lose();
        Pause();
        gameEnded=true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void PrepareRooms()
    {
        List<GameObject> rooms = new List<GameObject>();
        //Debug.Log(pumps.Length);

        for (int i=0; i<3; i++)
        {
            int indx = Random.Range(0, pumps.Length);
            while (pumpkins.Contains(pumps[indx])) indx = Random.Range(0, pumps.Length);
            GameObject temp = pumps[indx];

            temp.SetActive(true);
            //PumpkinDoorLink PDL = temp.GetComponent<PumpkinDoorLink>();

            pumpkins.Add(temp);
        }

        for (int i=0; i<2; i++)
        {
            int indx = Random.Range(0, clues.Length);
            while (clueRooms.Contains(clues[indx])) indx = Random.Range(0, clues.Length);
            GameObject temp = clues[indx];

            RoomManager CRM = temp.GetComponent<RoomManager>();
            foreach (GameObject k in CRM.keyInRoom)
            {
                k.SetActive(true);
            }

            clueRooms.Add(temp);
        }

        for (int i=0; i<2; i++)
        {
            rooms.Add(pumpkins[i]);
            rooms.Add(clueRooms[i]);
        }
        rooms.Add(pumpkins[2]);


        KeyController KC = firstKey.GetComponent<KeyController>();
        int j=0;
        foreach (GameObject r in rooms)
        {
            RoomManager CRM = r.GetComponent<RoomManager>();
            Debug.Log(CRM.doorDesc[0]);
            foreach (GameObject door in CRM.doorForRoom)
            {
                KC.doors.Add(door);
            }
            KC.desc = CRM.doorDesc[0];
            GameObject hint = CRM.pagesInRoom[0];
            hint.SetActive(true);
            hint.GetComponent<PaperController>().hint = lore[j++];

            KC = CRM.keyInRoom[0].GetComponent<KeyController>();
        }
        rooms[4].GetComponent<RoomManager>().keyInRoom[0].SetActive(false);
    }
}
