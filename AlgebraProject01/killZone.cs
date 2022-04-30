using UnityEngine;

public class killZone : MonoBehaviour
{
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If a player/entity enter in the trigger zone we kill him
        deathManager _deathManager;
        if ((_deathManager = collision.gameObject.GetComponentInChildren<deathManager>()) != null) // this also include main GameObject compenent
        {
            
            _deathManager.killObject();   // rotation validity is check in killObject         
        }
    }
}
