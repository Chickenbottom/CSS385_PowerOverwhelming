using UnityEngine;
using System.Collections;

public class IceWand : Weapon {

	private static GameObject projectilePrefab = null;
	
	public IceWand ()
	{
		Damage = 5;
		Range = 70f;
		ReloadTime = 2.0f;
		ReloadVariance = 0.1f;
		Accuracy = 0.8f;
		
		reloadTimer = ReloadTime;
		
		if (projectilePrefab == null)
			projectilePrefab = Resources.Load("Weapons/IceBombPrefab") as GameObject;
	}
	
	public Target src;
	protected override void PerformAttack(Target src, Target target)
	{
		if (target == null)
			return;
		//base.PerformAttack(src, target);
		reloadTimer = Random.Range (ReloadTime * (1f - ReloadVariance), ReloadTime * (1f + ReloadVariance));
		
		GameObject o = (GameObject) GameObject.Instantiate(projectilePrefab);
		IceBomb b = (IceBomb) o.GetComponent(typeof(IceBomb));
		b.transform.position = src.transform.position;
		b.SetParameters(src, target);
	}
}
