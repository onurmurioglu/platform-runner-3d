using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
   public static bool gameOver;

    public GameObject startingPanel;
    public GameObject startingText;

    public static bool isGameStarted;
    void Start()
    {
        gameOver = false;
        isGameStarted = false;
    }

     
    void Update()
    {
        if (gameOver)
        {
            //Time.timeScale = 0;
            //playerDeathPanel.SetActive(true);
            SceneManager.LoadScene(0);
        }

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingPanel);
            Destroy(startingText);
        }
    }
}
