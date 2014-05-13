using UnityEngine;
using System.Collections;

public enum Allegiance {
	kRodelle, 
	kAI,
}

public abstract class Target : MonoBehaviour, IDamagable
{
	public Allegiance Allegiance { 
		get { return mAllegiance; } 
		set { mAllegiance = value; }
	}
	
	protected Allegiance mAllegiance;
	
	public virtual Vector3 Position {
		get { return this.transform.position; }
		set { this.transform.position = value; }
	}
	
	public abstract void Damage(int damage);
}
