using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class singleCharacterController : MonoBehaviour
{

    private CharacterController control;

    Rigidbody rb;
    public CameraController cc;
    
    private Vector3 direction;
    public float forwardSpeed;

    public float laneDistance = 4;

    public float jumpForce;
    public float gravity = -3;

    public Transform player;

    public bool isGrounded;

    Vector3 firstPos, endPos;

    public Animator animator;

    public GameObject paintingText;


    void Start()
    {
       
        control = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!CharacterManager.isGameStarted)
        {
            return;

        }

        control.Move(direction * Time.fixedDeltaTime);

      
    }

    void Update()
    {
        if (!CharacterManager.isGameStarted)
        {
            return;
        }

        animator.SetBool("isGameStarted", true);

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
    
        control.center = control.center;
    }

    private void Jump()
    {
        direction.y = jumpForce;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
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

        if (hit.transform.tag == "EndGame")
        {
            cc.finish = true;
            paintingText.SetActive(true);

            animator.SetBool("isGameStarted", false);

            Destroy(GetComponent<SwipeManager>());
            Destroy(GetComponent<singleCharacterController>());
        }
    }

    IEnumerator retryGame()
    {
        yield return new WaitForSeconds(1f);
        CharacterManager.gameOver = true;
    }

    
}
