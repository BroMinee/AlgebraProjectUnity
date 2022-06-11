using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [SerializeField] private float minPositionOnLeft;
    [SerializeField] private float maxPositionOnRight;
    
    [SerializeField] private float minPositionUnder;
    [SerializeField] private float maxPositionAbove;
    private float smoothFactor = 2;
    private Vector3 dragOrigin;
    private GameObject player;
    [SerializeField] private Camera cam;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Missing value");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !Input.GetMouseButton(1))
        {

        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);


            

            Vector3 newPosition = transform.position + difference; // Follow only x and y position || Z will never change !

            newPosition.y = Mathf.Clamp(newPosition.y, minPositionUnder, maxPositionAbove); // clamp the value to no see outside of the world
            newPosition.x = Mathf.Clamp(newPosition.x, minPositionOnLeft, maxPositionOnRight); // clamp the value to no see outside of the world
            newPosition.z = -10;


            //Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = newPosition;
        }
        else if(!Input.GetKey(KeyCode.LeftControl))
        {
            float yPosition = Mathf.Clamp(player.transform.position.y, minPositionUnder, maxPositionAbove); // clamp the value to no see outside of the world
            float xPosition = Mathf.Clamp(player.transform.position.x, minPositionOnLeft, maxPositionOnRight); // clamp the value to no see outside of the world

            Vector3 targetPosition = new Vector3(xPosition, yPosition, transform.position.z); // Follow only x and y position || Z will never change !
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
        
    }

    
}
