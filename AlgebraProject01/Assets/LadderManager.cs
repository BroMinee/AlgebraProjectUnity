using UnityEngine;

public class LadderManager : MonoBehaviour
{
    private BoxCollider2D ladderCollider;
    // Start is called before the first frame update
    void Start()
    {
        ladderCollider = GetComponent<BoxCollider2D>();
        if(ladderCollider == null)
        {
            Debug.LogError("Missing component");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ControlerPlayer cp = collision.gameObject.GetComponent<ControlerPlayer>();
            cp.inRangeToClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ControlerPlayer cp = collision.gameObject.GetComponent<ControlerPlayer>();
            cp.inRangeToClimb = false;
            cp.isClimbing = false;
        }
    }
}
