using UnityEngine;
using System.Collections;

public interface Weapon
{
	int Damage { get; set; }
	float Range { get; set; }
	float ReloadTime { get; set; }
	
	float ReloadVariance { get; set; }
	float Accuracy { get; set; }
	
	void Attack(Target src, Target target);
}
