using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopBag : Distractions
{
    public override float SetObjectWeight()
    {
        objectWeight = 1.0f;
        return objectWeight;
    }
}
