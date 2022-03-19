using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathManager : MonoBehaviour
{
    private Transform tr;
    private RotationManager rotationManager;
    private Vector3 respawnPointPosition;
    private Quaternion respawnPointRotation;
    private SpriteRenderer gameObjectColor;
    private Rigidbody2D rb;
    // Start is called before the first frame update

    private void OnEnable()
    {
        respawnPointPosition = this.transform.position;
        respawnPointRotation = this.transform.rotation;

    }
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        gameObjectColor = GetComponent<SpriteRenderer>();
        rotationManager = FindObjectOfType<RotationManager>();
        if(tr == null || respawnPointPosition == null || rotationManager == null || gameObjectColor == null || rb == null || respawnPointRotation == null)
        {
            Debug.LogError("Missing components");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(tr.position.y < -13 || tr.position.y > 13)
        {
            
            respawnObject();
            if(tag.Equals("Player"))
            {
                rotationManager.ResetScreenRotation();
            }
            
        }
    }

    public void respawnObject()
    {
        rb.velocity = Vector3.zero;
        
        rb.angularVelocity = 0;
        tr.position = respawnPointPosition;
        tr.rotation = respawnPointRotation;
        StartCoroutine(BlinkOnRespawn());
    }

    IEnumerator BlinkOnRespawn()
    {
        Color color = gameObjectColor.color;
        gameObjectColor.color = new Color(color.r, color.g, color.b,0.5f);
        yield return new WaitForSeconds(0.35f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,1f);
        yield return new WaitForSeconds(0.35f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,0.5f);
        yield return new WaitForSeconds(0.3f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,1f);
        yield return new WaitForSeconds(0.2f);
        
        gameObjectColor.color = new Color(color.r, color.g, color.b,0.5f);
        yield return new WaitForSeconds(0.1f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,1f);
        yield return new WaitForSeconds(0.1f);
        
        gameObjectColor.color = new Color(color.r, color.g, color.b,0.5f);
        yield return new WaitForSeconds(0.1f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,1f);
        yield return new WaitForSeconds(0.1f);
        
        gameObjectColor.color = new Color(color.r, color.g, color.b,0.5f);
        yield return new WaitForSeconds(0.1f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,1f);
        yield return new WaitForSeconds(0.1f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,0.5f);
        yield return new WaitForSeconds(0.1f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,1f);
        yield return new WaitForSeconds(0.1f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,0.5f);
        yield return new WaitForSeconds(0.1f);

        gameObjectColor.color = new Color(color.r, color.g, color.b,1f);
        yield return new WaitForSeconds(0.1f);

    }
}
