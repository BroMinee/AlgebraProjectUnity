using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    [SerializeField] private int level;
    private bool playerIn;
    void Update()
    {
        if (!playerIn)
            return;
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Level"+level.ToString(),LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = false;
        }
    }
}

