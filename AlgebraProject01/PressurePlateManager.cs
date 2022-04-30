using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    [SerializeField] GameObject structToEnable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.ToLower().Contains("box"))
        {
            structToEnable.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.ToLower().Contains("Box"))
        {
            structToEnable.SetActive(false);
        }
    }
}
