using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AttackManager : MonoBehaviour
{

    public float attackRangeX;
    public float attackRangeY;
    public bool canAttack;
    public bool longRange = false;

    public void Start()
    {
        canAttack = true;
    }

    [SerializeField] public GameObject myPrefab;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] public GameObject attackParticle;
    public Vector3 offSet;

    public void GiveLongRange()
    {
        longRange = true;
        attackRangeX = 10;
        offSet.x = 5.7f;
    }

    public void Attack(ControlerPlayer Player)
    {
        StartCoroutine(startAttack());
        Collider2D[] enemy = Physics2D.OverlapBoxAll(attackPoint.position + offSet, new Vector2(attackRangeX, attackRangeY), 0, enemyLayer);
        
        for(int i = 0; i < enemy.Length; i++)
        {
            for(int j = i; j < enemy.Length;j++)
            {
                if (Vector2.Distance(enemy[i].transform.position,Player.transform.position) > Vector2.Distance(enemy[j].transform.position, Player.transform.position))
                {
                    (enemy[i],enemy[j]) = (enemy[j],enemy[i]); 
                }
            }
        }
           

            
        for(int i = 0; i < enemy.Length; i++)
        {
            Debug.Log("hit order: " + i + enemy[i].name + Vector2.Distance(Player.transform.position, enemy[i].transform.position));
        }
        foreach (Collider2D enemyCollider in enemy)
        {
            if(!(enemyCollider.name.Contains("Box") || enemyCollider.name.Contains("Enemy") || enemyCollider.name.Contains("Arrow")))
            {
                Debug.Log(enemyCollider.name + " don't contrain Box or Enemy or Arrow");
                continue;
            }
            Debug.Log("Hit:" + enemyCollider.name);

            if (enemyCollider.tag == "Enemy")
            {
                
                var enemyLife = enemyCollider.gameObject.GetComponentInChildren<EnemyAttackManager>();
                FindObjectOfType<AudioManager>().Play("slice");
                enemyLife.Die();
            }


            Rigidbody2D rb2D = enemyCollider.GetComponentInChildren<Rigidbody2D>();
            
            if(rb2D == null)
            {
                rb2D = enemyCollider.GetComponentInParent<Rigidbody2D>();
            }

            if (rb2D != null)
            {
                if (rb2D.gameObject.tag == "Arrow")
                {
                    
                    GameObject arrow = Instantiate(myPrefab, attackPoint.position + offSet, Quaternion.identity);
                    Rigidbody2D rb2dD = arrow.GetComponentInChildren<Rigidbody2D>();
                    if (rb2D.GetComponentInChildren<arrowManager>().isGoingLeft == true)
                    {
                        Debug.Log("Going right " + rb2D.GetComponentInChildren<arrowManager>().isGoingLeft);
                        rb2dD.AddForce(Vector2.right * 800);
                    }
                    else
                    {
                        Debug.Log("Going left " + rb2D.GetComponentInChildren<arrowManager>().isGoingLeft);
                        rb2dD.AddForce(Vector2.left * 800);
                        Transform child = arrow.GetComponentInChildren<Transform>();
                        child.transform.rotation = new Quaternion(0, 180, 0, 0);
                        arrow.GetComponentInChildren<arrowManager>().isGoingLeft = true;
                    }
                    arrow.GetComponentInChildren<arrowManager>().sendByPlayer = true;
                    Destroy(rb2D.gameObject);

                    StartCoroutine(ActivateGravity(rb2dD));
                    Destroy(arrow, 5);
                }
                else
                {
                    StartCoroutine(ApplyForce(rb2D, Player));
                }
                
                
                //rb2D.AddForce(new Vector2(Player.transform.localScale.x * Player.attackForce.x, 0));
            }

            return;
           
        }
    }

    IEnumerator ActivateGravity(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(0.4f);

        rb.gravityScale = 1;
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
        yield return new WaitForSeconds(0.15f);
        GameObject dust = Instantiate(attackParticle, attackPoint.position, Quaternion.identity);
        Quaternion dustRotation = dust.transform.rotation;
        if(offSet.x < 0)
        {
            dustRotation.y = 0;
        }
        else
        {
            dustRotation.y = 180;
        }
        dust.transform.rotation = dustRotation;
        if(longRange)
        {
            Destroy(dust, 0.6f);
        }
        else
        {
            Destroy(dust, 0.3f);
        }
        
        yield return new WaitForSeconds(0.6f);
        canAttack = true;

    }


}


