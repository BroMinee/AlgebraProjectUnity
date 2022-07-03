using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private float timeToLoad = 2f;
    [SerializeField] private int numberOfShoot = 1;
    [SerializeField] private float attackForce = 800;
    float distance = 15f;
    private bool isAttacking = false;
    [SerializeField] private GameObject origin;
    [SerializeField] private bool shootLeft = false;
    private bool canShoot = true;
    private bool isDead = false;
    [SerializeField] private Collider2D collider;

    [SerializeField] private GameObject myPrefab;
    bool isInRange()
    {
        if(shootLeft == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin.transform.position, Vector2.right, distance, layerMask);
            if (hit.collider != null)
            {
                return hit.collider.gameObject.tag == "Player";
            }
            return false;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(origin.transform.position, Vector2.left, distance, layerMask);
            if (hit.collider != null)
            {
                return hit.collider.gameObject.tag == "Player";
            }
            return false;
        }
        
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;
        if(canShoot == true && isAttacking == false && isInRange())
        {
            isAttacking= true;
            canShoot = false;
            StartCoroutine(InitialShoot());
        }
        
    }

    IEnumerator InitialShoot()
    {
        yield return new WaitForSeconds(timeToLoad);
        animator.SetBool("Attack", true);
        for(int i = 0; i < numberOfShoot; i++)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Shoot());
        }
        animator.SetBool("Attack", false);

    }

    IEnumerator Shoot()
    {
        
        yield return new WaitForSeconds(0.3f);
        GameObject arrow = Instantiate(myPrefab, origin.transform.position, Quaternion.identity);
        Rigidbody2D rb2d = arrow.GetComponentInChildren<Rigidbody2D>();
        if (shootLeft == false)
        {
            rb2d.AddForce(Vector2.right * attackForce);

        }
        else
        {
            rb2d.AddForce(Vector2.left * attackForce);
            Transform child = arrow.GetComponentInChildren<Transform>();
            child.transform.rotation = new Quaternion(0, 180, 0, 0);
            arrow.GetComponentInChildren<arrowManager>().isGoingLeft = true;


        }
        isAttacking = false;
        
        StartCoroutine(ActivateGravity(rb2d));
        Destroy(arrow, 5);
    }
    

    IEnumerator ActivateGravity(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(0.4f);
        
        rb.gravityScale = 1;
        canShoot = true;

    }

    public void Die()
    {
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 15);
        collider.enabled = false;
        isDead = true;
    }
}
