using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : ToyBehavior
{

   
    public override float SetObjectWeight()
    {
        objectWeight = 2.0f;
        return objectWeight;
    }
}
