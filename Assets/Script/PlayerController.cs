using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float jumpGravityModifier;
    public float fallGravityModifier = 3f;
    public bool isOnGround = true;
    public bool gameOver = false;
    private bool canDoubleJump = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= jumpGravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        } else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !gameOver) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = false;
        }

        if (playerRb.velocity.y < 0) {
            playerRb.velocity += new Vector3(0, Physics.gravity.y * (jumpGravityModifier - 2) * Time.deltaTime, 0);
        } else if (playerRb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            playerRb.velocity += new Vector3(0, Physics.gravity.y * (fallGravityModifier - 2) * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            canDoubleJump = true;
            isOnGround = true;

        } else {
            isOnGround = false;
        } 
    }

    private void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        } 
    }
}
