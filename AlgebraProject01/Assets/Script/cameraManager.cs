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
    private float  zPosition;
    private GameObject player;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        zPosition = transform.position.z;
        if (player == null)
        {
            Debug.LogError("Missing value");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float yPosition = Mathf.Clamp(player.transform.position.y, minPositionUnder, maxPositionAbove); // clamp the value to no see outside of the world
        float xPosition = Mathf.Clamp(player.transform.position.x, minPositionOnLeft, maxPositionOnRight); // clamp the value to no see outside of the world

        Vector3 targetPosition = new Vector3(xPosition, yPosition, zPosition); // Follow only x
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    
}
