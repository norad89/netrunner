using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameObject cubePlayer;
    public GameObject spherePlayer;
    public float jumpForce = 1200;
    public float jumpGravityModifier = 4f;
    public float fallGravityModifier = 3f;
    public bool canUsePowerUp = true;
    public bool isOnGround = true;
    float dashDuration = 0.25f; // Durata del dash in secondi
    float currentDashTime = 0f; // Tempo trascorso dallo start del dash
    private GameManager gameManager;


    void Start()
    {
        gameManager = GameManager.Instance;
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= jumpGravityModifier;
    }

    void Update()
    {

        if (gameManager.playerType == "cube")
        {
            cubePlayer.SetActive(true);
            spherePlayer.SetActive(false);
        }
        else if (gameManager.playerType == "sphere")
        {
            cubePlayer.SetActive(false);
            spherePlayer.SetActive(true);
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (isOnGround || (canUsePowerUp && gameManager.powerUpCount != 0)) && gameManager.isGameActive)
        {
            if (!isOnGround && gameManager.playerType == "cube")
            {
                // double jump
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
                canUsePowerUp = false;
                UIMainScene.Instance.UpdatePowerUpCount(--gameManager.powerUpCount);
            }
            else if (!isOnGround && gameManager.playerType == "sphere")
            {
                Vector3 dashDirection = transform.right;
                float dashForce = 1500f;

                // Annulla la velocità verticale corrente per evitare ulteriori salti
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
                playerRb.AddForce(dashDirection * dashForce, ForceMode.Impulse);

                canUsePowerUp = false;
                UIMainScene.Instance.UpdatePowerUpCount(--gameManager.powerUpCount);

                // Disabilita la gravità temporaneamente durante il dash
                playerRb.useGravity = false;

                // Resetta il timer del dash
                currentDashTime = 0f;
            }

            // manages single and double jump for cube and single jump for sphere
            if (gameManager.playerType == "cube" || isOnGround && gameManager.playerType == "sphere")
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
        }

        // gravity push while jumping
        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (jumpGravityModifier - 1) * Time.deltaTime;
        }
        // gravity push when stop jumping
        else if (playerRb.velocity.y > 0 && (!Input.GetKey(KeyCode.Space)))
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (fallGravityModifier - 1) * Time.deltaTime;
        }

        if (!isOnGround && gameManager.playerType == "sphere")
        {
            // Incrementa il timer del dash
            currentDashTime += Time.deltaTime;

            // Se il timer supera la durata del dash, riattiva la gravità
            if (currentDashTime >= dashDuration)
            {
                playerRb.useGravity = true;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (gameManager.isGameActive)
        {
            if (collision.gameObject.CompareTag("StartingPlatform") && collision.contacts[0].normal.x >= 0)
            {
                isOnGround = true;
                canUsePowerUp = true;
            }
            else if (collision.gameObject.CompareTag("Platform") && collision.contacts[0].normal.x >= 0)
            {
                isOnGround = true;
                canUsePowerUp = true;
            }
            else if (collision.contacts[0].normal.x < 0)
            {
                gameManager.gameOverTriggered = true;
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
