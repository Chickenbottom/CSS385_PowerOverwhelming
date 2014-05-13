using UnityEngine;
using System.Collections;

public class Sword : Weapon 
{
	private static GameObject mProjectilePrefab = null;
	
	public Sword ()
	{
		Damage = 5;
		Range = 12f;
		ReloadTime = 1.0f;
		ReloadVariance = 0.5f;
		Accuracy = 0.8f;
		
		mReloadTimer = ReloadTime;
		
		if (mProjectilePrefab == null)
			mProjectilePrefab = Resources.Load("Weapons/ArrowPrefab") as GameObject;
	}
	
	protected override void PerformAttack(Target src, Target target)
	{
		if (target == null)
			return;
		base.PerformAttack(src, target);
		
		GameObject o = (GameObject) GameObject.Instantiate(mProjectilePrefab);
		Arrow a = (Arrow) o.GetComponent(typeof(Arrow));
		
		a.transform.position = src.transform.position;		
		a.SetDestination(target.transform.position);
	}
}
