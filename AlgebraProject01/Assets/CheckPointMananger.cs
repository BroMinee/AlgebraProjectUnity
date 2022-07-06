using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMananger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particleSystem;
    private bool taken = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (taken)
            return;
        if(collision.tag == "Player" && collision.gameObject.name.Contains("Player"))
        {
            animator.SetBool("taken", true);
            collision.gameObject.GetComponentInChildren<deathManager>().setRespawnPoint();
            FindObjectOfType<AudioManager>().Play("getbonus");
            taken = true;
            particleSystem.Play();
        }
    }
}
