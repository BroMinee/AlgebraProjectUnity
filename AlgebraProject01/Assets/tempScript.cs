using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;


public class tempScript : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] string msg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            if (collision.tag.Equals("Player") && collision.gameObject.name == "Player")
            {
                
                    text.text = msg; //"Press 'E' to end the level";
                    text.text = msg; //"Press 'F' to rotate the world";
                    text.text = msg; // "Use WASD to move around";

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
                    if (msg == "Press 'E' to end the level")
                    {
                        text.text = "GG";
                    }
                }
                
                
            }
        }
    }

}
