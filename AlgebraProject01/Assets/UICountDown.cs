using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;


public class UICountDown : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;

    public void SetCountTo(int i)
    {
        if(i == 0)
        {
            m_Text.text = "";
        }

        else if (i == 1)
        {
            m_Text.text = i.ToString() + " second remaining.";
        }
        else
        {
            m_Text.text = i.ToString() + " seconds remaining.";
        }
        FindObjectOfType<AudioManager>().Play("CountDown");
    }
}
