using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : ToyBehavior
{

    public override float SetObjectWeight()
    {
        objectWeight = 1.0f;
        return objectWeight;
    }
}
