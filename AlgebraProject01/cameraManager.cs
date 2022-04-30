using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [SerializeField] private float minPositionOnLeft;
    [SerializeField] private float maxPositionOnRight;
    
    [SerializeField] private float minPositionUnder;
    [SerializeField] private float maxPositionAbove;
    private float smoothFactor = 3;
    
    private GameObject player;



   
    bool isInFreeCam;
    private Vector3 camPos;




    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Missing value");
        }
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(Input.GetKeyDown(KeyCode.LeftControl));
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isInFreeCam = !isInFreeCam;
            if (isInFreeCam)
            {
                camPos = transform.position;
            }
            else
            {
                transform.position = camPos;
            }
        }

        if (isInFreeCam)
        {

        }
        else
        {
            float yPosition = Mathf.Clamp(player.transform.position.y, minPositionUnder, maxPositionAbove); // clamp the value to no see outside of the world
            float xPosition = Mathf.Clamp(player.transform.position.x, minPositionOnLeft, maxPositionOnRight); // clamp the value to no see outside of the world

            Vector3 targetPosition = new Vector3(xPosition, yPosition, transform.position.z); // Follow only x and y position || Z will never change !
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }

    }

    
}
