using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private Transform playerTransform; // Riferimento al transform del giocatore
    public float destroyDistance = 30f; // Distanza a cui distruggere l'oggetto

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
    }

    void Update()
    {
        // Verifica se l'oggetto si trova a sinistra del giocatore
        if (transform.position.x < playerTransform.position.x - destroyDistance)
        {
            // Distrugge l'oggetto
            Destroy(gameObject);
        }
    }
}