using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private float distance;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if(playerTransform == null)
        {
            Debug.LogError("detectPlayer: playerTransform is null");
        }
    }

    private void Update()
    {
        isPlayerInRange();
    }


    void isPlayerInRange()
    {
        RaycastHit2D[] castStar = Physics2D.CircleCastAll(transform.position, distance, Vector2.zero);
        foreach (RaycastHit2D raycastHit in castStar)
        {
            if(raycastHit.collider == null)
                continue;

            if(raycastHit.collider.gameObject.tag == "Player")
            {
                Debug.Log(raycastHit.collider.gameObject.name + " has been hit");
            }
        }
    }
}
