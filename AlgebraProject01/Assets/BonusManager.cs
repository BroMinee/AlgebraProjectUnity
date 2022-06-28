using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var am =collision.gameObject.GetComponentInChildren<AttackManager>();
            am.canAttack = true;
            Destroy(gameObject);
        }
    }
}
