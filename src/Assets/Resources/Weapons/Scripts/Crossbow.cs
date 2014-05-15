﻿using UnityEngine;
using System.Collections;

public class Crossbow : Weapon
{
	private static GameObject mProjectilePrefab = null;
		
	public Crossbow ()
	{
		Damage = 5;
		Range = 45f;
		ReloadTime = 2.0f;
		ReloadVariance = 0.3f;
		Accuracy = 0.8f;
	
		reloadTimer = ReloadTime;
		
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