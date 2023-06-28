using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.tag == "DoubleJump")
            {
                if (gameManager.powerUpCount < 3)
                {
                    gameManager.powerUpCount = gameManager.powerUpCount++;            
                    UIMainScene.Instance.UpdatePowerUpCount(gameManager.powerUpCount++);
                }
                Destroy(gameObject);
            }
            else if (gameObject.tag == "DashForward")
            {
                // player.canDashForward = true;
                Destroy(gameObject);
            }
        }

    }
}
