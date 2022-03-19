using UnityEngine;

public class signText : MonoBehaviour
{
    [SerializeField] string text;
    [SerializeField] string author;
    private DialogueManager UISign;

    private void Start()
    {
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        if(UI == null)
        {
            Debug.Log("Not found UI tag");
        }
        UISign = UI.GetComponentInChildren<DialogueManager>();
        if(UISign == null || text == null || author == null)
        {
            Debug.LogError("Missing field");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is BoxCollider2D)
        {
            if (collision.tag.Equals("Player"))
            {
                UISign.ShowText(text,author);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UISign.UnshowText();
    }

}
