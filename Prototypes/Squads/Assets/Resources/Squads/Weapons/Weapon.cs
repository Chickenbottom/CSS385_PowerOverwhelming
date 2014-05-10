using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public int Damage { get; set; }
	public float Range { get; set; }
	public float ReloadTime { get; set; }
	
	public float ReloadVariance { get; set; }
	public float Accuracy { get; set; }
	
	protected float mReloadTimer;
		
	public virtual void Attack(Target src, Target target)
	{
		if (target == null || src == null)
			return;
		
		mReloadTimer -= Time.deltaTime;
		if (mReloadTimer < 0) {
			PerformAttack(src, target);
		}
	}
	
	public virtual void Reset()
	{
		mReloadTimer = Random.Range (ReloadTime * (1f - ReloadVariance), ReloadTime * (1f + ReloadVariance));
	}
	
	protected virtual void PerformAttack(Target src, Target target)
	{
		if (target == null)
			return;
		
		mReloadTimer = Random.Range (ReloadTime * (1f - ReloadVariance), ReloadTime * (1f + ReloadVariance));
		
		Unit e = (Unit) target.GetComponent(typeof(Unit));
		e.Damage(this.Damage);
	}
}
