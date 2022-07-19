using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintTheWall : MonoBehaviour
{
   
    public GameObject brush;
    public float brushSize = 0.03f;
    
    public GameObject player;

    void Start()
    {
        
    }


    void Update()
    {
  
        if (player.transform.position.z >= 372)
        {
            Debug.Log(player.transform.position);

            if (Input.GetMouseButton(0))
            {
                StartCoroutine(nextLevel());

                var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(Ray, out hit))
                {                    
                    var paint = Instantiate(brush, hit.point + Vector3.down * 0.03f, brush.transform.rotation, transform);
                    paint.transform.localScale = Vector3.one * brushSize;
                }
            }
        }
    }

    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(25f);
        SceneManager.LoadScene(1);
    }
}
