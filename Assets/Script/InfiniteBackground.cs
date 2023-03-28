using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject backGroundPrefab;
    private GameObject player;
    private float backgroundWidth = 552.5f;
    private bool backgroundSpawned;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        backgroundSpawned = false;
    }

    void Update()
    {
        Vector3 backgroundSpawnPos = new Vector3(transform.position.x + backgroundWidth, transform.position.y, transform.position.z);

        if (player.transform.position.x > transform.position.x && !backgroundSpawned)
        {
            backgroundSpawned = true;
            GameObject newBackground = Instantiate(backGroundPrefab, backgroundSpawnPos, backGroundPrefab.transform.rotation);
        }
        else if (player.transform.position.x > transform.position.x + backgroundWidth)
        {
            Destroy(gameObject);
        }
    }

}