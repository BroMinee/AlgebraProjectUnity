using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        deathManager _deathManager;
        if ((_deathManager = collision.gameObject.GetComponentInChildren<deathManager>()) != null)
        {
            _deathManager.killObject();
            
        }
        else if((_deathManager = collision.gameObject.GetComponent<deathManager>()) != null)
        {
            _deathManager.killObject();
            
        }

    }
}
