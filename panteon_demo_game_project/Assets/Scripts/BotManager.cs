using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BotManager : MonoBehaviour
{
    public string[] Finish;
   public float[] distances;
    public BotController[] bots;
    public TextMeshProUGUI leaderText,finishText;
    public multiCharacterControl pl;
    public static BotManager instance;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        distances = new float[11];
        InvokeRepeating("DistanceUpdate", 0.01f, 0.01f);
        InvokeRepeating("FinishLeader", 0.1f, 0.1f);
   //     InvokeRepeating("DistanceLeader", 0.1f, 0.1f);
    }
    public void DistanceUpdate()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            distances[i] = bots[i].mesafe;
            distances[10] = pl.mesafe;
            if (i == bots.Length - 1)
            {
                DistanceLeader();
            }
        }
    }
    public void FinishLeader()
    {
        finishText.text = "";
        for (int i = 0; i < Finish.Length; i++)
        {
            if (Finish[i]!="")
            {
                finishText.text = finishText.text + System.Environment.NewLine + Finish[i];
            }
        }
    }
    public void DistanceLeader()
    {
   
        for (int i = 0; i < distances.Length; i++)
        {
            float gecici = 0;

            for (int k = 0; k < 10; k++)
            {
                for (int j = k + 1; j <= 10; j++)
                {
                    if (distances[k] > distances[j])
                    {
                        gecici = distances[j];
                        distances[j] = distances[k];
                        distances[k] = gecici;
                    }
                }
            }
            leaderText.text = "";
            for (int a = 0; a < 11; a++)
            {
                for (int c = 0; c < 11; c++)
                {
                    try
                    {
                        if (pl.mesafe == distances[a])
                        {

                            leaderText.text = leaderText.text + System.Environment.NewLine + "Player";
                            break;
                            // print(distances[a]);
                        }
                        if (bots[c].mesafe == distances[a])
                            
                        {

                            leaderText.text = leaderText.text + System.Environment.NewLine + bots[c].name;
                            break;
                            // print(distances[a]);
                        }
                      
                    }
                    catch (System.Exception)
                    {

                      
                    }
                   
                   
                }
            }
        }
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
