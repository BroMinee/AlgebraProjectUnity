using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class DialogueManager : MonoBehaviour
{
    
    [SerializeField] public TMP_Text author;
    [SerializeField] public GameObject DialogueUI;
    
    
    [SerializeField] public TMP_Text m_Text;

    private void Start()
    {
        if(m_Text == null ||  author == null || DialogueUI == null)
        {
            Debug.LogError("Missing field");
        }

        DialogueUI.SetActive(false);
    }
    public void ShowText(string msg,string authorName)
    {
        DialogueUI.SetActive(true);
        author.text = authorName;
        m_Text.text = "";
          
        StartCoroutine(ShowTextSlowly(msg));
    }
    
    IEnumerator ShowTextSlowly(string message)
    {
        StringBuilder decomposeMsg = new StringBuilder(message);
        for (int i = 0; i < message.Length; i++)
        {

            decomposeMsg[i] = ' ';
            m_Text.text = decomposeMsg.ToString();

        }

        

        for (int i = 0; i < message.Length; i++)
        {
            
            decomposeMsg[i] = message[i];
            m_Text.text = decomposeMsg.ToString();
            yield return new WaitForSeconds(0.01f);
            
        }
        
    }
    public void UnshowText()
    {
        
        DialogueUI.SetActive(false);
    }
}
