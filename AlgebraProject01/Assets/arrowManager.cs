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
        if(collision.gameObject.tag == "Ground")
        {
            
            
            Rigidbody2D rb = collision.gameObject.GetComponentInChildren<Rigidbody2D>();
            
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.AddForce(new Vector2(0, 0));

        }
    }
}

