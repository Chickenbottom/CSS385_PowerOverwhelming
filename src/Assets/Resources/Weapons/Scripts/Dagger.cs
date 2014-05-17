﻿using UnityEngine;
using System.Collections;

public class Dagger : Weapon
{       
    public Dagger ()
    {
        Damage = 2;
        Range = 12f;
        ReloadTime = 2.0f;
        ReloadVariance = 0.5f;
        Accuracy = 0.8f;
        
        reloadTimer = ReloadTime;
    }
}
