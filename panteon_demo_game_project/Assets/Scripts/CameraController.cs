using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 offset;
    public GameObject player;

    public Transform target;
    public float t;

    public bool camState = false;
    public bool finish;

    public Transform s;
    void Start()
    {
        offset = transform.position - player.transform.position;
    }



    void LateUpdate()
    {

        if (finish == false)
        {
            transform.position = player.transform.position + offset;
        }
        else
        {
            camState = true;
        }

    }

    private void FixedUpdate()
    {
        if (camState == true/*Camera.main.transform.position.z >= 363*/)
        {
            Vector3 a = transform.position;
            Vector3 b = target.position;
            transform.position = Vector3.Lerp(a, b, t);

            transform.rotation = s.rotation;

        }
    }

    //private void OnCollisionEnter(Collision cam)
    //{
    //    if (cam.gameObject.tag == "camPosChange")
    //    {
    //        camState = true;
    //        Debug.Log("Temas etti");
    //    }
    //}
}
