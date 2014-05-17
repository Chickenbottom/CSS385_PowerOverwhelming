using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
	public Allegiance GetAllegiance { get { return mAllegiance; } }
	
	protected Allegiance mAllegiance;
	
	public enum Allegiance {
		kRodelle, kAI
	}
}
