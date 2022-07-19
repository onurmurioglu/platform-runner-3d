using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public int[] sayilar;
    public float mesafe;
    public GameObject finish;
    public string nameBot;
    public Rigidbody rb;
    public bool isTranslate;
    public Vector3 startPositionn;
    void Start()
    {
        startPositionn = gameObject.transform.position;
        nameBot = gameObject.name;
        speed = Random.Range(8, 12);                                                                                                                                
    }

    void Update()
    {
        if (isTranslate)
        {

        }
        else
        {
            mesafe = Vector3.Distance(gameObject.transform.position, finish.transform.position);
        }
    }
    private void FixedUpdate()
    {
        if (!CharacterManager.isGameStarted)
        {
          
            return;

        }
        else
        {
            animator.SetBool("isGameStarted", true);
        }
        if (isTranslate==false)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            if (transform.position.y < 0.1f)
               
            {
               
                transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
                
            }
        }
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="JumpZone")
        {
            int random = Random.Range(0, 20);

            if (random<16)
            {
                rb.useGravity = true;
                rb.AddForce(Vector3.up * 400);
                Invoke("Close", 1.625f);
            }
        }
        if (other.gameObject.tag == "FinishArea")
        {
            isTranslate = true;

            for (int i = 0; i < BotManager.instance.Finish.Length; i++)
            {
                if (BotManager.instance.Finish[i] == "")
                {
                    BotManager.instance.Finish[i] = gameObject.name;
                    break;
                }
            }
        }
        if (other.gameObject.tag == "Obstacle")
        {

            //transform.position = startPosition.position;
            transform.position = new Vector3(startPositionn.x, startPositionn.y, startPositionn.z);
        }
    }
    void Close()
    {
       //animator.enabled = true;
        rb.useGravity = false;
        rb.isKinematic = true;
        Invoke("UnClose", 0.2f);
    }
    void UnClose()
    {
        rb.isKinematic = false;
    }
}
