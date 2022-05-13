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
            Debug.Log("Enemy hit :" + enemyCollider.name);
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


