using UnityEngine;
using System.Collections;

public class Crossbow : MonoBehaviour, Weapon
{
	public int Damage { get; set; }
	public float Range { get; set; }
	public float ReloadTime { get; set; }
	
	public float ReloadVariance { get; set; }
	public float Accuracy { get; set; }

	private GameObject mProjectilePrefab = null;
	private float mReloadTimer;
		
	public void Awake ()
	{
		Damage = 3;
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
	
	public void Attack(Target src, Target target)
	{
		if (target == null || src == null)
			return;
		
		mReloadTimer -= Time.deltaTime;
		if (mReloadTimer < 0) {
			this.PerformAttack(src, target);
		}
	}
	
	protected void PerformAttack(Target src, Target target)
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
