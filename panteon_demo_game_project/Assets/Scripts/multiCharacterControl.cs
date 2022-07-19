using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class multiCharacterControl : MonoBehaviour
{

    private CharacterController control;

    Rigidbody rb;

    //private int desiredLane = 2;
    private Vector3 direction;
    public float forwardSpeed;

    public float laneDistance = 4;

    public float mesafe;
    bool finishis;
    public GameObject finish;

    public float jumpForce;
    public float gravity = -3;
    public Transform player;

    public bool isGrounded;

    Vector3 firstPos, endPos;

    public Animator animator;

    public GameObject finishText;
    public GameObject finishPanel;

    public Vector3 startPositionn;

    public bool isTranslate;
    void Start()
    {
        startPositionn = gameObject.transform.position;
        
        control = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

          
        if (!CharacterManager.isGameStarted)
        {
            return;

        }
        mesafe = Vector3.Distance(gameObject.transform.position, finish.transform.position);
        if (isTranslate==false)
        {
            control.Move(direction * Time.fixedDeltaTime);
        }
    
    }

    void Update()
    {

        if (!CharacterManager.isGameStarted)
        {
            return;
        }
       
        animator.SetBool("isGameStarted", true);
        if (isTranslate==false)
        {
            if (control.isGrounded)
            {
                direction.y = -2;
                if (SwipeManager.swipeUp)
                {
                    animator.SetBool("isGrounded", isGrounded);
                    Jump();
                }
            }
            else
            {
                animator.SetBool("isGrounded", !isGrounded);
                direction.y += gravity * Time.deltaTime;
            }


            if (Input.GetMouseButtonDown(0))
            {
                firstPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                endPos = Input.mousePosition;

                float farkX = endPos.x - firstPos.x;

                transform.Translate(farkX * Time.deltaTime * forwardSpeed / 40, 0, 0);
            }

            if (Input.GetMouseButtonUp(0))
            {
                firstPos = Vector3.zero;
                endPos = Vector3.zero;
            }

            direction.z = forwardSpeed;
            direction.y += gravity * Time.deltaTime;

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            //transform.localPosition = /*targetPosition;*/ Vector3.MoveTowards(transform.position, targetPosition, 150 * Time.deltaTime);
            control.center = control.center;
            if (Input.GetKeyDown(KeyCode.Space))
            {

                direction.y = -2;

                animator.SetBool("isGrounded", isGrounded);
                Jump();


            }
        }
        else
        {
            animator.SetBool("isGameStarted", false);
        }
    }

    private void Jump()
    {
        direction.y = jumpForce; 

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            transform.position = new Vector3(startPositionn.x, startPositionn.y, startPositionn.z);

        }
        if (other.transform.tag == "FinishArea")
        {
           
            isTranslate = true;

            StartCoroutine(endGame());

            for (int i = 0; i < BotManager.instance.Finish.Length; i++)
            {
                if (BotManager.instance.Finish[i] == ""&& finishis==false)
                {
                    BotManager.instance.Finish[i] = "Player";
                    finishis = true;
                    break;
                }
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag=="Obstacle")
        {
            forwardSpeed = 0;
            CharacterManager.gameOver = true;
        }

        if (hit.transform.tag == "StickObstacle")
        {
            forwardSpeed = -12;
           
            StartCoroutine(retryGame());

        }

        if (hit.transform.tag == "rotatingPlatform")
        {
            rb.AddForce(player.right * 14, ForceMode.Impulse);           
        }
 
    }

    IEnumerator retryGame()
    {
        yield return new WaitForSeconds(1f);
        CharacterManager.gameOver = true;
    }

    IEnumerator endGame()
    {
        finishPanel.SetActive(true);
        finishText.SetActive(true);
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene(0);
    }
}
