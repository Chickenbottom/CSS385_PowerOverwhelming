using UnityEngine;
using System.Collections;

public class Sword : Weapon 
{
	private static GameObject projectilePrefab = null;
	
	public Sword ()
	{
		Damage = 3;
		Range = 12f;
		ReloadTime = 1.0f;
		ReloadVariance = 0.5f;
		Accuracy = 0.8f;
		
		reloadTimer = ReloadTime;
		
		if (projectilePrefab == null)
			projectilePrefab = Resources.Load("Weapons/ArrowPrefab") as GameObject;
	}
	
	protected override void PerformAttack(Target src, Target target)
	{
		if (target == null)
			return;
		base.PerformAttack(src, target);
		
		GameObject o = (GameObject) GameObject.Instantiate(projectilePrefab);
		Arrow a = (Arrow) o.GetComponent(typeof(Arrow));
		
		a.transform.position = src.transform.position;		
		a.SetDestination(target.transform.position);
	}
}
