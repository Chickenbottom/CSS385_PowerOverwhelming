using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public Progressbar KingsHealthBar;
	public Progressbar SwordsmanExperienceBar;
	public Progressbar ArcherExperienceBar;
	public Progressbar MageExperienceBar;

	public AudioSource mMusic;
	//private float mMusicVolume = 1;
	//private float mSFXVolume = 1;
  
  public int CurrentLevel;
    
	public void OnGUI()
	{
		if (GUI.Button(new Rect(120, 35, 150, 50), "Return to Level Selector"))
			Application.LoadLevel("LevelLoader");
			
		GUI.Label(new Rect(135, 65, 150, 50), "Left-Alt to spawn");
	}
	
	private Dictionary<UnitType, Progressbar> mExpBars;
	
    void Awake()
    {
        GameState.CurrentLevel = this.CurrentLevel;
    }
    
	void Start()
	{
		if (KingsHealthBar == null || SwordsmanExperienceBar == null || 
		    MageExperienceBar == null || ArcherExperienceBar == null) {
		    Debug.LogError("Experience and health bars need to be attached to the game manager!");
		}
		
		mExpBars = new Dictionary<UnitType, Progressbar>();
		mExpBars.Add(UnitType.Archer, ArcherExperienceBar);
		mExpBars.Add(UnitType.Swordsman, SwordsmanExperienceBar);
		mExpBars.Add(UnitType.Mage, MageExperienceBar);
		
		foreach(UnitType u in mExpBars.Keys) {
			mExpBars[u].maxValue = GameState.RequiredUnitExperience[u];
		}
		
		KingsHealthBar.maxValue = GameState.KingsHealth;        
	}
	
	void Update()
	{
		foreach(UnitType u in mExpBars.Keys) {
			mExpBars[u].UpdateValue(GameState.UnitExperience[u]);
		}
		
		KingsHealthBar.UpdateValue(GameState.KingsHealth);
        
        if (Input.GetKeyDown("a"))
            Time.timeScale += 0.5f;
	}
	public void SetMusicVolume(float v){
		mMusic.volume = v;\
	}
}
