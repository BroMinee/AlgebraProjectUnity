using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class cheatManager : MonoBehaviour
{ 

    void Start()
    {
        DontDestroyOnLoad(this);
    }




    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("Level1" , LoadSceneMode.Single);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene("Level3", LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            SceneManager.LoadScene("Level4", LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            SceneManager.LoadScene("Level5", LoadSceneMode.Single);
        }

    }
}
