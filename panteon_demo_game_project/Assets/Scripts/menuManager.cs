using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class menuManager : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("Oyundan çýkýldý");
        Application.Quit();
    }
}
