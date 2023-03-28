using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject backGroundPrefab;
    private GameObject player;
    private float backgroundWidth = 540;
    private bool backgroundSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        Vector3 backgroundSpawnPos = new Vector3(transform.position.x + backgroundWidth, transform.position.y, transform.position.z);

        if (player.transform.position.x > transform.position.x && !backgroundSpawned)
        {
            GameObject newBackground = Instantiate(backGroundPrefab, backgroundSpawnPos, backGroundPrefab.transform.rotation);
            backgroundSpawned = true;
        }
        else if (player.transform.position.x > transform.position.x + backgroundWidth)
        {
            Destroy(gameObject);
        }
    }

}