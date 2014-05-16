using UnityEngine;
using System.Collections;

public class Progressbar : MonoBehaviour {
	
	public int Value { 
		get { return currentValue; }
		set { UpdateValue(value); }
	}
	
	public float box_length;
	public GameObject cur_obj;
	public SpriteRenderer Base;
	Rect guiBox; 
	
	private Texture2D background;
	private Texture2D foreground;
	
	public int currentValue;
	public int maxValue;
	
	void Start()
	{
		//mGuiBox = new Rect(cur_obj.transform.position.x, 10, 200, 20);
		Bounds b = Base.bounds;
		guiBox = new Rect(b.min.x, b.min.y, b.size.x, b.size.y);
		
		Vector3 topLeft = Camera.main.WorldToScreenPoint(new Vector3(b.min.x, b.min.y, 0f));
		Vector3 bottomRight = Camera.main.WorldToScreenPoint(new Vector3(b.max.x, b.max.y, 0f));
		
		guiBox.xMax = bottomRight.x + 1f;
		guiBox.yMin = Screen.height - bottomRight.y + 1f;
		
		guiBox.xMin = topLeft.x;
		guiBox.yMax = Screen.height - topLeft.y;
		
		background = new Texture2D(1, 1, TextureFormat.RGB24, false);
		foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);
		
		background.SetPixel(0, 0, Color.red);
		foreground.SetPixel(0, 0, Color.green);
		
		background.Apply();
		foreground.Apply();
		Base.enabled = false;
	}
	
	public void UpdateValue(int value)
	{
		currentValue = value;
		if (currentValue > maxValue) currentValue = maxValue;
		if (currentValue < 0) currentValue = 0;
	}
	
	void OnGUI()
	{
		GUI.BeginGroup(guiBox);
		{
			GUI.DrawTexture(new Rect(0, 0, guiBox.width, guiBox.height), background, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(0, 0, guiBox.width*currentValue/maxValue, guiBox.height), foreground, ScaleMode.StretchToFill);
		}
		GUI.EndGroup(); ;
	}
}
