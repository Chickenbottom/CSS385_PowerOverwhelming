using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherEnemyBehavior : MonoBehaviour 
{
	private int kDefaultHealth = 4;
	private int mHealth;
	private int mPreviousHealth;
	
	private List<Sprite> mSprites;
	// Use this for initialization
	void Start () 
	{
		mHealth = kDefaultHealth;
		mPreviousHealth = mHealth;
		
		mSprites = new List<Sprite>();
		mSprites.Add (Resources.Load("Textures/EnemySwordsman/e_swordsman", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/EnemySwordsman/e_swordsman1", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/EnemySwordsman/e_swordsman2", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/EnemySwordsman/e_swordsman3", typeof(Sprite)) as Sprite);
		mSprites.Add (Resources.Load("Textures/EnemySwordsman/e_swordsman", typeof(Sprite)) as Sprite);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mHealth != mPreviousHealth) {
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			sr.sprite = mSprites[mHealth];
		}
		
		mPreviousHealth = mHealth;
	}
	
	public void Damage(int damage)
	{
		mHealth -= damage;
		if (mHealth <= 0) {
			Destroy(this.gameObject);
			Destroy (this);
		}
	}
}
