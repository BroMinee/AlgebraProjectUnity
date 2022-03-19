using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGroundManager : MonoBehaviour
{
    private Transform tr;
    [SerializeField] private BoxCollider2D insideBox;
    private deathManager deathManager;
    private void Start()
    {
        deathManager = GetComponent<deathManager>();
        tr = GetComponent<Transform>();
        if(tr == null || insideBox == null || deathManager == null)
        {
            Debug.LogError("Missing component");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            deathManager.respawnObject();
        }
    }

}
