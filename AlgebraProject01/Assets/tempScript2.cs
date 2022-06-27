using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempScript2 : MonoBehaviour
{
    [SerializeField] private float minPositionOnLeft;
    [SerializeField] private float maxPositionOnRight;

    [SerializeField] private float minPositionUnder;
    [SerializeField] private float maxPositionAbove;
    private float smoothFactor = 2;
    private Vector3 dragOrigin;
    [SerializeField] private Camera cam;
    private GameObject pl;
    void Start()
    {

        pl = GameObject.FindGameObjectWithTag("Player");
        if (pl == null)
        {
            Debug.LogError("Missing value");
        }
    }

    // Update is called once per frame
    void Update()
    {

        float yPosition = pl.transform.position.y; // clamp the value to no see outside of the world
        float xPosition = pl.transform.position.x; // clamp the value to no see outside of the world

        Vector3 targetPosition = new Vector3(xPosition, yPosition, transform.position.z); // Follow only x and y position || Z will never change !
        //Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = targetPosition;

    }


}
