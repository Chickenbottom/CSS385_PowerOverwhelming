﻿using UnityEngine;
using System.Collections;

public class Crossbow : Weapon
{
	private GameObject mProjectilePrefab = null;
		
	public void Awake ()
	{
		Damage = 5;
		Range = 40f;
		ReloadTime = 2.0f;
		ReloadVariance = 0.3f;
		Accuracy = 0.8f;
	
		mReloadTimer = ReloadTime;
	}
	
	public void Start()
	{
		if (mProjectilePrefab == null)
			mProjectilePrefab = Resources.Load("Squads/Prefabs/ArrowPrefab") as GameObject;
	}
	
	protected override void PerformAttack(Target src, Target target)
	{
		if (target == null)
			return;
			
		mReloadTimer = Random.Range (ReloadTime * (1f - ReloadVariance), ReloadTime * (1f + ReloadVariance));
		
		Unit e = (Unit) target.GetComponent(typeof(Unit));
		e.Damage(this.Damage);
		
		GameObject o = (GameObject) Instantiate(mProjectilePrefab);
		Arrow a = (Arrow) o.GetComponent(typeof(Arrow));
		
		a.transform.position = src.transform.position;		
		a.SetDestination(target.transform.position);
	}
}
