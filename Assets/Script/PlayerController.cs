using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 1200;
    public float jumpGravityModifier;
    public float fallGravityModifier = 3f;
    public bool canDoubleJump = true;
    public bool isOnGround = true;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= jumpGravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || (canDoubleJump && gameManager.powerUpCount != 0)) && gameManager.isGameActive)
        {
            if (!isOnGround)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
                canDoubleJump = false;
                gameManager.UpdatePowerUpCount(--gameManager.powerUpCount);
            }
            
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (jumpGravityModifier - 1) * Time.deltaTime;
        }
        else if (playerRb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (fallGravityModifier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (gameManager.isGameActive)
        {
            if (collision.gameObject.CompareTag("StartingPlatform") && collision.contacts[0].normal.x >= 0)
            {
                isOnGround = true;
                canDoubleJump = true;
                Destroy(collision.gameObject, 6f);
            }
            else if (collision.gameObject.CompareTag("Platform") && collision.contacts[0].normal.x >= 0)
            {
                isOnGround = true;
                canDoubleJump = true;
                Destroy(collision.gameObject, 2f);
            }
            else if (collision.contacts[0].normal.x < 0)
            {
                gameManager.GameOver();
                Debug.Log("Game Over - Collisione dal lato sinistro del cubo");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("StartingPlatform"))
        {
            isOnGround = false;
        }
    }
}
