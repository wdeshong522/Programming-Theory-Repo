using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : ToyBehavior
{
  
    public override float SetObjectWeight()
    {
        objectWeight = 4.0f;
        return objectWeight;
    }
}
