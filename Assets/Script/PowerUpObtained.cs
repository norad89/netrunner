using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObtained : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.tag == "DoubleJump") {
                // If the GameObject's name matches the one you suggest, destroy it
                player.canDoubleJump = true;
                Destroy(gameObject);
            } else if (gameObject.tag == "DashForward") {
                // player.canDashForward = true;
                Destroy(gameObject);
            }
        }

    }
}
