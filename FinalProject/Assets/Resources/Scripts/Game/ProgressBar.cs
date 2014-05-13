using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	public float box_length;
	public GameObject cur_obj;
	Rect box; 
	

	private Texture2D background;
	private Texture2D foreground;
	
	public float progress = 100;
	public int maxHealth = 100;
	
	void Start()
	{
		box = new Rect(cur_obj.transform.position, 10, 200, 20);
		background = new Texture2D(1, 1, TextureFormat.RGB24, false);
		foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);
		
		background.SetPixel(0, 0, Color.red);
		foreground.SetPixel(0, 0, Color.green);
		
		background.Apply();
		foreground.Apply();
	}
	
	void Update()
	{
		TowerBehavior tower = GameObject.Find ("mShield").GetComponent<TowerBehavior>();
		
		progress = tower.health;
		if (progress > 100) progress = maxHealth;
		if (progress < 0) progress = 0;
	}
	
	void OnGUI()
	{
		GUI.BeginGroup(box);
		{
			GUI.DrawTexture(new Rect(0, 0, box.width, box.height), background, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(0, 0, box.width*progress/maxHealth, box.height), foreground, ScaleMode.StretchToFill);
		}
		GUI.EndGroup(); ;
	}
}
