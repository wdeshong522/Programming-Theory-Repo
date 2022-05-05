using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBehavior : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerControllerScript;
    public float toyWeight;
    public bool changedSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        //Ignores collision with 
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.pickedUp == true && changedSpeed == false)
        {
            float newSpeed = playerControllerScript.playerSpeed - toyWeight;
            playerControllerScript.playerSpeed = newSpeed;
            changedSpeed = true;
        }
    }
}
