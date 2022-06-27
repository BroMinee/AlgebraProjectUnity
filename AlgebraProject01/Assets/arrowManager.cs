using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var dm = collision.gameObject.GetComponentInChildren<deathManager>();
            dm.killObject();
        }
    }

}
