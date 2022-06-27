using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private float timeToLoad = 2f;
    [SerializeField] private int numberOfShoot = 1;
    float distance = 15f;
    private bool isAttacking = false;
    [SerializeField] private GameObject origin;
    [SerializeField] private bool shootLeft = false;
    private bool canShoot = true;

    [SerializeField] private GameObject myPrefab;
    bool isInRange()
    {
        if(shootLeft == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin.transform.position, Vector2.right, distance);
            if (hit.collider != null)
            {
                return hit.collider.gameObject.tag == "Player";
            }
            return false;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(origin.transform.position, Vector2.left, distance);
            if (hit.collider != null)
            {
                return hit.collider.gameObject.tag == "Player";
            }
            return false;
        }
        
    }

    private void FixedUpdate()
    {
        if(canShoot == true && isAttacking == false && isInRange())
        {
            isAttacking= true;
            canShoot = false;
            StartCoroutine(InitialShoot());
        }
        else if (isAttacking == true && !isInRange())
        {
            isAttacking = false;
            animator.SetBool("Attack", false);
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
        
    }

    IEnumerator Shoot()
    {
        
        yield return new WaitForSeconds(0.3f);
        GameObject arrow = Instantiate(myPrefab, origin.transform.position, Quaternion.identity);
        Rigidbody2D rb2d = arrow.GetComponentInChildren<Rigidbody2D>();
        if (shootLeft == false)
        {
            rb2d.AddForce(Vector2.right * 800);

        }
        else
        {
            rb2d.AddForce(Vector2.left * 800);
            arrow.transform.rotation = new Quaternion(0, 180, 0, 0);

        }
        StartCoroutine(ActivateGravity(rb2d));
        Destroy(arrow, 5);
    }
    

    IEnumerator ActivateGravity(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(0.4f);
        
        rb.gravityScale = 1;
        canShoot = true;

    }
}
