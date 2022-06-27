using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;


public class tempScript : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] int v;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            if (collision.tag.Equals("Player") && collision.gameObject.name == "Player")
            {
                if(v == 0)
                {
                    text.text = "Press 'E' to end the level";
                }
                else if (v == 1)
                {
                    text.text = "Press 'F' to rotate the world";
                }
                else if(v == 2)
                {
                    text.text = "Use WASD to move around";
                
                }

            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            if (collision.tag.Equals("Player") && collision.gameObject.name == "Player")
            {
                text.text = "";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            if (collision.tag.Equals("Player") && collision.gameObject.name == "Player")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (v == 0)
                    {
                        text.text = "GG";
                    }
                }
                
                
            }
        }
    }

}
