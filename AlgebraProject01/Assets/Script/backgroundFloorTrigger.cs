using UnityEngine;

public class backgroundFloorTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D floorCollider;

    private void Start()
    {
        if (floorCollider == null)
        {
            Debug.LogError("Missing componeents");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            ActiveGround();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            
            DisactiveGround();
        }
    }


    public void ActiveGround()
    { 
        floorCollider.isTrigger = false;
    }

    public void DisactiveGround()
    {
        floorCollider.isTrigger = true;
    }
}
