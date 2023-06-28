using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public GameObject player;
    public GameObject startingPlatform;


    private void Awake()
    {
        GameManager.Instance.player = player;
        GameManager.Instance.startingPlatform = startingPlatform;
    }
}
