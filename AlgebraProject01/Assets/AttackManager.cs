using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    [SerializeField] public BoxCollider2D attackRange;
   

    public void Attack(float xPlayer)
    {
        
        List<Rigidbody2D>  a=  GetObjectsInBoxCollider(attackRange);
        Debug.Log(a);
        foreach (Rigidbody2D r in a)
        {
            Debug.Log(r.position.x - xPlayer);
            if(r.position.x - xPlayer > 0)
            {
                r.AddForce(new Vector2(20 , 0));
            }
            else
            {
                r.AddForce(new Vector2(-20, 0));
            }
            
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public List<Rigidbody2D> GetObjectsInBoxCollider(Collider2D collider)
    {
        List<Rigidbody2D> objectToMove = new List<Rigidbody2D>();


        Collider2D coll = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(coll, filter, results);
        foreach (var hitCollider in results)
        {
            Rigidbody2D rb = hitCollider.gameObject.GetComponent<Rigidbody2D>();
            if (rb == null)
                continue;

            objectToMove.Add(rb);

        }

        return objectToMove;

    }
}
