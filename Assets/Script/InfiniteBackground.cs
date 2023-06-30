using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject backGroundPrefab;
        private float backgroundWidth;
    private bool backgroundSpawned;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        backgroundSpawned = false;
        backgroundWidth = backGroundPrefab.GetComponent<BoxCollider>().size.x;
    }

    void Update()
    {
        if (gameManager.player != null)
        {
            Vector3 backgroundSpawnPos = new Vector3(transform.position.x + backgroundWidth, transform.position.y, transform.position.z);

            if (gameManager.player.transform.position.x > transform.position.x && !backgroundSpawned)
            {
                backgroundSpawned = true;
                GameObject newBackground = Instantiate(backGroundPrefab, backgroundSpawnPos, backGroundPrefab.transform.rotation);
            }
            else if (gameManager.player.transform.position.x > transform.position.x + backgroundWidth)
            {
                Destroy(gameObject);
            }
        }
    }
}