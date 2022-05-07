using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI CounterText;
    public int Count;

    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Count -= 1;
        CounterText.text = "Toys Remaining: " + Count;
        Destroy(other.gameObject);
        playerControllerScript.playerSpeed = playerControllerScript.defaultSpeed;
    }
}