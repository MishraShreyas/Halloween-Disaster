using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject controlPage;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleControls();
        }
    }

    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ToggleControls()
    {
        controlPage.SetActive(!controlPage.activeSelf);
    }

    public void doExitGame()
    {
     Application.Quit();
    }
}
