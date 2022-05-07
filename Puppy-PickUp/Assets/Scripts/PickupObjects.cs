using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PickupObjects : MonoBehaviour
{
    public float objectWeight;
    
    public virtual float SetObjectWeight()
    {
        objectWeight = 1.0f;
        return objectWeight;
    }

}
