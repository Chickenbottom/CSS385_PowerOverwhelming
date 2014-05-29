using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlPanel : MonoBehaviour
{
    public Progressbar KingsHealthBar;
    public Progressbar SwordsmanExperienceBar;
    public Progressbar ArcherExperienceBar;
    public Progressbar MageExperienceBar;
    
    public Progressbar HealCooldownBar;
    public Progressbar AoeCooldownBar;
    public Progressbar BoostCooldownBar;
    
    public GUIText SwordsmanLevel;
    public GUIText ArcherLevel;
    public GUIText MageLevel;
    
    public GUIText GoldCounterText;
    public GUIText WaveCounter;
    public AudioSource Music;
       
    public AbilityTower HealTower;
    public AbilityTower AoeTower;
    public AbilityTower BoostTower;
       
	//private float mMusicVolume = 1;
	//private float mSFXVolume = 1;
    
    private Dictionary<UnitType, Progressbar> mExpBars;
    private Dictionary<UnitType, GUIText> mLevelText;

    void Awake ()
    {
        if (HealTower == null)
            Destroy (HealCooldownBar);
            
        if (AoeTower == null)
            Destroy (AoeCooldownBar);
            
        if (BoostTower == null)
            Destroy (BoostCooldownBar);
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
        
        UpdateCooldownBar(HealCooldownBar, HealTower);
        UpdateCooldownBar(AoeCooldownBar, AoeTower);
        UpdateCooldownBar(BoostCooldownBar, BoostTower);
        
        KingsHealthBar.UpdateValue (GameState.KingsHealth);
        GoldCounterText.text = GameState.Gold.ToString ();
        WaveCounter.text = GameState.RemainingWaves.ToString ();
        
        if (GameState.IsDebug && Input.GetKeyDown ("a"))
            Time.timeScale += 0.5f;
            
        if (GameState.IsDebug && Input.GetKeyDown ("s"))
            Time.timeScale -= 0.5f;
	}
    
    private void UpdateCooldownBar(Progressbar cooldownBar, AbilityTower tower)
    {
        if (tower != null) {
            cooldownBar.MaxValue = (int)(tower.ability.CoolDown * 100);
            cooldownBar.UpdateValue((int)(tower.ability.CooldownTimer * 100));
        }
    }
    
	public void SetMusicVolume(float v)
    {
		Music.volume = v;
	}
}
