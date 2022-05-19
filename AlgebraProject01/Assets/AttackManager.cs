using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackManager : MonoBehaviour
{

    public float attackRangeX;
    public float attackRangeY;
    public bool canAttack;

    public void Start()
    {
        canAttack = true;
    }

    [SerializeField] public Transform attackPoint;
    [SerializeField] public LayerMask enemyLayer;
    public Vector3 offSet;

    public void Attack(ControlerPlayer Player)
    {
        StartCoroutine(startAttack());
        Collider2D[] enemy = Physics2D.OverlapBoxAll(attackPoint.position + offSet, new Vector2(attackRangeX, attackRangeY), 0, enemyLayer);

        foreach (Collider2D enemyCollider in enemy)
        {
            Rigidbody2D rb2D = enemyCollider.GetComponent<Rigidbody2D>();
            if(rb2D != null)
            {
                StartCoroutine(ApplyForce(rb2D, Player));
                
                //rb2D.AddForce(new Vector2(Player.transform.localScale.x * Player.attackForce.x, 0));
            }
        }
    }

    IEnumerator ApplyForce(Rigidbody2D rb2D,ControlerPlayer Player)
    {
        
        yield return new WaitForSeconds(0.2f);
        if (Player.facingRight)
        {
            rb2D.AddForceAtPosition(new Vector2(Player.attackForce.x, Player.attackForce.y), rb2D.gameObject.transform.position - new Vector3(0.9f,0,0));
            //rb2D.AddForce(new Vector2(Player.attackForce.x, Player.attackForce.y));
        }
        else
        {
            rb2D.AddForceAtPosition(new Vector2(Player.attackForce.x, Player.attackForce.y), rb2D.gameObject.transform.position - new Vector3(-0.9f, 0, 0));
            rb2D.AddForce(new Vector2(-Player.attackForce.x, Player.attackForce.y));
        }
        Debug.Log("Attacking " + rb2D.name);
    }


    public void changeOffSet(bool isRight)
    {
        if (isRight)
        {

            offSet.x = Mathf.Abs(offSet.x);
        }
        else
        {

            offSet.x = Mathf.Abs(offSet.x) * -1;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position + offSet, new Vector3(attackRangeX, attackRangeY, 0));
    }

    IEnumerator startAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.75f);
        canAttack = true;

    }
}


