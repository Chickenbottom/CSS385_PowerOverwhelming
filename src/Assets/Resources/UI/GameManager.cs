using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int CurrentLevel;
    public Progressbar KingsHealthBar;
    public Progressbar SwordsmanExperienceBar;
    public Progressbar ArcherExperienceBar;
    public Progressbar MageExperienceBar;
    
    public GUIText SwordsmanLevel;
    public GUIText ArcherLevel;
    public GUIText MageLevel;
    
    public GUIText GoldCounterText;
    public GUIText WaveCounter;
    public AudioSource Music;
       
	//private float mMusicVolume = 1;
	//private float mSFXVolume = 1;
    
    private Dictionary<UnitType, Progressbar> mExpBars;
    private Dictionary<UnitType, GUIText> mLevelText;
	
    void Awake()
    {
        GameState.CurrentLevel = this.CurrentLevel;
    }
    
    void Start ()
    {
        if (KingsHealthBar == null || SwordsmanExperienceBar == null || 
            MageExperienceBar == null || ArcherExperienceBar == null) {
            Debug.LogError ("Experience and health bars need to be attached to the game manager!");
        }
        
        mExpBars = new Dictionary<UnitType, Progressbar> ();
        mExpBars.Add (UnitType.Archer, ArcherExperienceBar);
        mExpBars.Add (UnitType.Swordsman, SwordsmanExperienceBar);
        mExpBars.Add (UnitType.Mage, MageExperienceBar);
        
        mLevelText = new Dictionary<UnitType, GUIText>();
        mLevelText.Add(UnitType.Archer, ArcherLevel);
        mLevelText.Add(UnitType.Swordsman, SwordsmanLevel);
        mLevelText.Add(UnitType.Mage, MageLevel);
        
        foreach (UnitType u in mExpBars.Keys) {
            mExpBars [u].MaxValue = GameState.RequiredUnitExperience [u];
        }
        
        KingsHealthBar.MaxValue = GameState.KingsHealth;
    }
    
    void Update ()
    {
        foreach (UnitType u in mExpBars.Keys) {
            mExpBars [u].MaxValue = UnitUpgrades.GetExpToNextLevel(u);
            mExpBars [u].UpdateValue ((int)UnitUpgrades.GetStat(u, UnitStat.Experience));
            mLevelText [u].text = ((int)UnitUpgrades.GetStat(u, UnitStat.Level)).ToString();
        }
        
        KingsHealthBar.UpdateValue (GameState.KingsHealth);
        GoldCounterText.text = GameState.Gold.ToString ();
        WaveCounter.text = GameState.RemainingWaves.ToString ();
        
        if (Input.GetKeyDown ("a"))
            Time.timeScale += 0.5f;
            
        if (Input.GetKeyDown ("s"))
            Time.timeScale -= 0.5f;
	}
    
	public void SetMusicVolume(float v)
    {
		Music.volume = v;
	}
}
