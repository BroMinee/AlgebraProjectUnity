using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManagement : MonoBehaviour
{

    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private CircleCollider2D circleCollider2D;

    private void Start()
    {
        if(boxCollider2D == null|| circleCollider2D == null)
        {
            Debug.LogError("SerializeField Missing");
        }
    }

    public void DisableCollider()
    {
        boxCollider2D.enabled = false;
        circleCollider2D.enabled = false;
    }public void EnableCollider()
    {
        boxCollider2D.enabled = true;
        circleCollider2D.enabled = true;
    }

}
