using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public bool isGoingLeft = false;
    public bool sendByPlayer = false;

    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play("arrow");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(rb.constraints == RigidbodyConstraints2D.FreezeAll)
        { return; }
        if (collision.tag == "Enemy")
        {
            
            if(sendByPlayer)
            {
                var enemyLife = collision.gameObject.GetComponentInChildren<EnemyAttackManager>();
                enemyLife.Die();
            }
            
        }
       
        else if (collision.gameObject.tag == "Player")
        {
            var dm = collision.gameObject.GetComponentInChildren<deathManager>();
            dm.KillInstante();
            
        }
        else if(collision.gameObject.tag == "Ground")
        {
            
            
            Rigidbody2D rb = gameObject.GetComponentInChildren<Rigidbody2D>();
            
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.AddForce(new Vector2(0, 0));

        }
        else if (collision.gameObject.tag == "Entity")
        {
            Rigidbody2D rb = gameObject.GetComponentInChildren<Rigidbody2D>();

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.AddForce(new Vector2(0, 0));
            Debug.Log(collision.gameObject.name);
            var dm = collision.gameObject.GetComponentInChildren<deathManager>();
            dm.damageObject();

        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        
        if (collision.gameObject.tag == "Entity")
        {
            Rigidbody2D rb = gameObject.GetComponentInChildren<Rigidbody2D>();

            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(new Vector2(0, 0));

            

        }


    }

}

