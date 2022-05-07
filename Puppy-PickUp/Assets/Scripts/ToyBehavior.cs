using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBehavior : PickupObjects
{
    public Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
        Physics.IgnoreCollision(playerCollider, GetComponent<Collider>());
    }

    public override float SetObjectWeight()
    {
        objectWeight = 1.0f;
        return objectWeight;
    }
}
