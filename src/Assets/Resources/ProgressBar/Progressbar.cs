using UnityEngine;
using System.Collections;

public class Progressbar : MonoBehaviour {
	
	public float box_length;
	public GameObject cur_obj;
	public SpriteRenderer Base;
	Rect mGuiBox; 
	
	private Texture2D background;
	private Texture2D foreground;
	
	public int CurrentValue;
	public int MaxValue;
	
	void Start()
	{
		//mGuiBox = new Rect(cur_obj.transform.position.x, 10, 200, 20);
		Bounds b = Base.bounds;
		mGuiBox = new Rect(b.min.x, b.min.y, b.size.x, b.size.y);
		
		Vector3 topLeft = Camera.main.WorldToScreenPoint(new Vector3(b.min.x, b.min.y, 0f));
		Vector3 bottomRight = Camera.main.WorldToScreenPoint(new Vector3(b.max.x, b.max.y, 0f));
		
		mGuiBox.xMax = bottomRight.x + 1f;
		mGuiBox.yMin = Screen.height - bottomRight.y + 1f;
		
		mGuiBox.xMin = topLeft.x;
		mGuiBox.yMax = Screen.height - topLeft.y;
		
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
		CurrentValue = value;
		if (CurrentValue > MaxValue) CurrentValue = MaxValue;
		if (CurrentValue < 0) CurrentValue = 0;
	}
	
	void OnGUI()
	{
		GUI.BeginGroup(mGuiBox);
		{
			GUI.DrawTexture(new Rect(0, 0, mGuiBox.width, mGuiBox.height), background, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(0, 0, mGuiBox.width*CurrentValue/MaxValue, mGuiBox.height), foreground, ScaleMode.StretchToFill);
		}
		GUI.EndGroup(); ;
	}
}
