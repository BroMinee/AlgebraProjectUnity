using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private GameObject World; // The game TileMap
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> objectToNotRotate;
    [SerializeField] private Transform player;

    private bool isRotating = false;
    

    private void Start()
    {
        if(World == null || cam == null)
        {
            Debug.LogError("SerializeField Missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isRotating == false) // Detect player Input
        {
            StartCoroutine(ReverseScreen(World.transform.rotation.x == 0));
        }

        // Do the rotation
        if (isRotating) {
            DisableRigidBodyAndCollider(objectToNotRotate);
        }
           

    }

    IEnumerator ReverseScreen(bool Flip)
    {
        isRotating = true;
        Color initialColor = cam.backgroundColor;
        if (Flip)
        {
            World.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            World.transform.rotation = new Quaternion(180, 0, 0, 0);
        }


        
        
        for (float i = 0; i <= 100; i ++)
        {
            if (Flip)
            {
                cam.backgroundColor = new Color(initialColor.r + (0.367f- initialColor.r)*(i/100),initialColor.g + (0.0f - initialColor.g)*(i/100),initialColor.b + (0.0f - initialColor.b)*(i/100));
            }
            else
            {
                cam.backgroundColor = new Color(initialColor.r + (0.46f - initialColor.r) * (i / 100), initialColor.g + (0.46f - initialColor.g) * (i / 100), initialColor.b + (0.46f - initialColor.b) * (i / 100));
            }
            //World.transform.RotateAround(player.position, Vector3.up, 180f * i / 100f * Time.fixedDeltaTime);
            World.transform.Rotate(new Vector3(180f * i / 100f * Time.fixedDeltaTime, 0, 0));
            yield return new WaitForSeconds(0.001f);
            
        }


        EnableCollider(objectToNotRotate);
        isRotating = false;

        if(Flip)
        {
            cam.backgroundColor = new Color(0.367f, 0, 0);
            World.transform.rotation = new Quaternion(180, 0, 0, 0);
        }
        else
        {
            cam.backgroundColor = new Color(0.46f, 0.46f, 0.46f);
            World.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        
    }



    public void ResetScreenRotation()
    {
        cam.backgroundColor = new Color(118, 117, 117);
        EnableCollider(objectToNotRotate);
        isRotating = false;
        if(World.transform.rotation.x != 0)
        {
            StartCoroutine(ReverseScreen(World.transform.rotation.x == 0));
        }
        
        World.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void EnableCollider(List<GameObject> gms)
    {
        foreach (GameObject gm in gms)
        {
            foreach (Collider2D cl in gm.GetComponentsInChildren<Collider2D>())
            {
                cl.enabled = true;
            }

            foreach (Collider2D cl in gm.GetComponents<Collider2D>())
            {
                cl.enabled = true;
            }

            foreach (CircleCollider2D cc in gm.GetComponents<CircleCollider2D>())
            {
                cc.enabled = true;
            }


        }


    }

    private void DisableRigidBodyAndCollider(List<GameObject> gms)
    {
        foreach (GameObject gm in gms)
        {
                      

            foreach(Rigidbody2D rb in gm.GetComponentsInChildren<Rigidbody2D>())
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = 0;
            }

            foreach (Rigidbody2D rb in gm.GetComponents<Rigidbody2D>())
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = 0;
            }

            foreach (Collider2D cl in gm.GetComponentsInChildren<Collider2D>())
            {
                cl.enabled = false;
            }
            
            foreach (Collider2D cl in gm.GetComponents<Collider2D>())
            {
                cl.enabled = false;
            }
            
            foreach (CircleCollider2D cc in gm.GetComponents<CircleCollider2D>())
            {
                cc.enabled = false;
            }

            




        }


    }

}
