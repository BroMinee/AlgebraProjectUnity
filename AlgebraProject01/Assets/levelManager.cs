using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    [SerializeField] private int level;
    private bool playerIn;
    private DialogueManager UISign;
    GameObject UI;

    private void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
    }

    void Update()
    {
        if (!playerIn)
            return;
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(level == -1)
            {
                UISign = UI.GetComponentInChildren<DialogueManager>();
                UISign.ShowText("GG your have complete the tutorial (there is nothing more in this build)", "Tutorial");
            }
            else
            {
                SceneManager.LoadScene("Level" + level.ToString(), LoadSceneMode.Single);
            }
            
            
            
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

    public void JoinSolo()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MysteryGame()
    {
        SceneManager.LoadScene("MysteryGame", LoadSceneMode.Single);
    }

}

