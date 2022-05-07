using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frisbee : ToyBehavior
{
 
    public override float SetObjectWeight()
    {
        objectWeight = 3.0f;
        return objectWeight;
    }
}
