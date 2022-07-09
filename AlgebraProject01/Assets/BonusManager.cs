using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] bool PickWeapon = false;
    [SerializeField] bool PickJump = false;
    [SerializeField] bool PickSlowFalling = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var am =collision.gameObject.GetComponentInChildren<AttackManager>();
            var playerController = collision.gameObject.GetComponentInChildren<ControlerPlayer>();
            if(PickWeapon)
            {
                am.canAttack = true;
                playerController.HasLongRangeSword();
            }
            else if(PickJump)
            {
                playerController.EnableDoubleJump();
            }
            else if(PickSlowFalling)
            {
                playerController.EnableSlowFalling();
            }
            FindObjectOfType<AudioManager>().Play("getbonus");
            gameObject.SetActive(false);
        }
    }

    
}
