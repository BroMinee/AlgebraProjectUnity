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
        canAttack = false;
    }

    [SerializeField] public GameObject myPrefab;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public LayerMask enemyLayer;
    public Vector3 offSet;

    public void Attack(ControlerPlayer Player)
    {
        StartCoroutine(startAttack());
        Collider2D[] enemy = Physics2D.OverlapBoxAll(attackPoint.position + offSet, new Vector2(attackRangeX, attackRangeY), 0, enemyLayer);


        foreach (Collider2D enemyCollider in enemy)
        {
            
            if(enemyCollider.tag == "Enemy")
            {
                Debug.Log("Enemy hit");
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
        yield return new WaitForSeconds(0.75f);
        canAttack = true;

    }
}


