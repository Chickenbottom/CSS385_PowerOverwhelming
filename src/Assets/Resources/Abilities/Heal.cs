using UnityEngine;
using System.Collections;

public class Heal : Ability {

    public override void UsePositionAbility(Vector3 mousePos)
    {
        // add healing effects
        Debug.Log ("Healing location: " + mousePos);
    }

	public override void UseTargetedAbility(Target target)
	{
		if (target is Tower)
			Debug.Log ("Healing Tower: " + target);
		if (target is King)
			Debug.Log ("Healing King: " + target);
	}
}
