using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private GameObject World; // The game TileMap
    [SerializeField] private Rigidbody2D PlayerRB; // Player Rigid body
    [SerializeField] private ColliderManagement colliderManagement; // Script on Player GameObject which manager collider
 
    
    private int rotationSpeed = 150;
    private int RotationState = 0; // 0 no mouvement // 1 go to 0° // 2 go to 180°


    private void Start()
    {
        if(World == null || PlayerRB == null || colliderManagement == null)
        {
            Debug.LogError("SerializeField Missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Detect player Input
        {
            colliderManagement.DisableCollider();
            if (World.transform.rotation.z == 0)
            {
                RotationState = 2;
            }
            else if (World.transform.rotation.z == 1)
            {
                RotationState = 1;
            }
        }

        // Do the rotation
        if (RotationState == 1) { ReverseScreen(); }
        else if (RotationState == 2) { UnReverseScreen(); }       

    }

    void ReverseScreen()
    {
        if (World.transform.rotation.z > 0.0001)
        {
            PlayerRB.velocity = Vector3.zero;
            World.transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
        }
        else
        {
            
            RotationState = 0;
            World.transform.rotation = new Quaternion(0, 0, 0, 0);
            colliderManagement.EnableCollider();
            
        }
    }

    void UnReverseScreen()
    {
        if (World.transform.rotation.z < 0.9999)
        {
            PlayerRB.velocity = Vector3.zero;
            World.transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
            
        }
        else
        {
            RotationState = 0;
            World.transform.rotation = new Quaternion(0, 0, 180, 0);
            colliderManagement.EnableCollider();
        }
    }
}
