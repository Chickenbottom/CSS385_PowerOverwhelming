﻿using UnityEngine;
using System.Collections;

public enum Allegiance {
	Rodelle, 
	AI,
}

public abstract class Target : MonoBehaviour
{
    public Allegiance Allegiance 
    {
        get { return mAllegiance; }
        set { mAllegiance = value; }
    }
	
	public virtual Vector3 Position 
	{
		get { return this.transform.position; }
		set { this.transform.position = value; }
	}
	
	public abstract void Damage(int damage);
	
	protected int mHealth;
	protected Allegiance mAllegiance;
}
