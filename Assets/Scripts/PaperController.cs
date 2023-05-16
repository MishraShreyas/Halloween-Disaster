using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperController : MonoBehaviour
{

    public string hint;

    private UIManager UIM;
    private GameManager GM;
    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void Interact()
    {
        UIM.OpenHint(hint);
        GM.Pause();
    }
}
