using UnityEngine;
using System.Collections;

public enum Allegiance {
	kRodelle, 
	kAI,
}

public class Target : MonoBehaviour
{
	public Allegiance Allegiance { 
		get { return mAllegiance; } 
		set { mAllegiance = value; }
	}
	
	protected Allegiance mAllegiance;
}
