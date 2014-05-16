using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {

    // Abilities won't work on the towers that they belong to (at least heal won't)
	public abstract void UsePositionAbility(Vector3 mousePos);
	public abstract void UseTargetedAbility(Target target);
}
