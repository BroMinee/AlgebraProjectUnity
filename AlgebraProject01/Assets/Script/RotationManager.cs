using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private GameObject World; // The game TileMap
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> objectToNotRotate;
    [SerializeField] private List<GameObject> listBonus;
    [SerializeField] private Transform player;

    public bool isRotating = false;
    



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

            StartCoroutine(ReverseScreenCoroutine(World.transform.rotation.x == 0));
        }

        // Do the rotation
        if (isRotating) {
            DisableRigidBodyAndCollider();
        }
           

    }

    IEnumerator ReverseScreenCoroutine(bool Flip)
    {
        FindObjectOfType<AudioManager>().Play("rotate");
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


        EnableCollider();
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

    IEnumerator ResetScreenRotationCoroutine()
    {
        FindObjectOfType<AudioManager>().Play("rotate");
        isRotating = true;
        Color initialColor = cam.backgroundColor;
        
        World.transform.rotation = new Quaternion(180, 0, 0, 0);
        




        for (float i = 0; i <= 100; i++)
        {
            
            cam.backgroundColor = new Color(initialColor.r + (0.46f - initialColor.r) * (i / 100), initialColor.g + (0.46f - initialColor.g) * (i / 100), initialColor.b + (0.46f - initialColor.b) * (i / 100));
            
            //World.transform.RotateAround(player.position, Vector3.up, 180f * i / 100f * Time.fixedDeltaTime);
            World.transform.Rotate(new Vector3(180f * i / 100f * Time.fixedDeltaTime, 0, 0));
            yield return new WaitForSeconds(0.001f);

        }


        
        isRotating = false;

        
        cam.backgroundColor = new Color(0.46f, 0.46f, 0.46f);
        World.transform.rotation = new Quaternion(0, 0, 0, 0);
        

    }




    public void ResetScreenRotation()
    {

        isRotating = false;
        if(!(World.transform.rotation.x == 0 ||  World.transform.rotation.x == -180 || World.transform.rotation.x == 180 || World.transform.rotation.x == -0))
        {
            
            StartCoroutine(ResetScreenRotationCoroutine());
        }
        
        
    }

    public void EnableCollider()
    {

        

        foreach (GameObject gm in objectToNotRotate)
        {
            foreach (Collider2D cl in gm.GetComponentsInChildren<Collider2D>())
            {
                cl.enabled = true;
            }

            

            foreach (CapsuleCollider2D cc in gm.GetComponentsInChildren<CapsuleCollider2D>())
            {
                cc.enabled = true;
            }

            
            foreach (Rigidbody2D rb in gm.GetComponentsInChildren<Rigidbody2D>())
            {

                rb.constraints = RigidbodyConstraints2D.None;
                if(rb.gameObject.tag.Equals("Player"))
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    
                }
                rb.AddForce(new Vector2(0, 0));

            }
        }


    }

    private void DisableRigidBodyAndCollider()
    {
        foreach (GameObject gm in objectToNotRotate)
        {
                      

            foreach(Rigidbody2D rb in gm.GetComponentsInChildren<Rigidbody2D>())
            {
                
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

            }

            

            foreach (Collider2D cl in gm.GetComponentsInChildren<Collider2D>())
            {
                cl.enabled = false;
            }
            
            
            
            foreach (CapsuleCollider2D cc in gm.GetComponentsInChildren<CapsuleCollider2D>())
            {
                cc.enabled = false;
            }
        }


    }


    public void ReactiveBonus()
    {

        foreach(GameObject gm in listBonus)
        {
            gm.SetActive(true);
        }
    }
}
