using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool ChoosePink;
    public GameObject[] menuObjects;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        menuObjects[0].SetActive(true);
        menuObjects[1].SetActive(false);
        menuObjects[2].SetActive(false);
    }
    public void StartGame()
    {
        menuObjects[0].SetActive(false);
        menuObjects[1].SetActive(true);
        menuObjects[2].SetActive(false);
    }
    public void SettingsGame()
    {
        menuObjects[0].SetActive(false);
        menuObjects[1].SetActive(false);
        menuObjects[2].SetActive(true);
    }
    public void Back()
    {
        menuObjects[0].SetActive(true);
        menuObjects[1].SetActive(false);
        menuObjects[2].SetActive(false);
    }
    public void QuitGame()
    {

        Application.Quit();
    }
    public void Classic()
    {
        ChoosePink = false;
        SceneManager.LoadScene("GamePlay");

    }
    public void Pink()
    {
        ChoosePink = true;
        SceneManager.LoadScene("GamePlay");

    }
}
