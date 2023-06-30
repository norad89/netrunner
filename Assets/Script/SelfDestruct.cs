using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private GameManager gameManager;
    private Transform playerTransform; // Riferimento al transform del giocatore
    public float destroyDistance = 30f; // Distanza a cui distruggere l'oggetto

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (gameManager.player) {
      
        // Verifica se l'oggetto si trova a sinistra del giocatore
        if (transform.position.x < gameManager.player.transform.position.x - destroyDistance)
        {
            // Distrugge l'oggetto
            Destroy(gameObject);
        }
        }
    }
}