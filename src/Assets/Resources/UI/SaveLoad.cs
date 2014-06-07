using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour {

    public enum SAVEFILE
    {
        Level,
        Upgrade
    }

    private string levelPath;
    private string upgradePath;
    // Level file contains
    // line 1: highest level
    // line 2: gold
    private List<string> levelStrings;
    private List<string> upgradeStrings;

    bool loadSuccess;

    void Awake()
    {
        Instantiate();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Instantiate()
    {
        levelPath = "game.save";
        upgradePath = "upgrades.save";
        levelStrings = new List<string>();
        upgradeStrings = new List<string>();
        loadSuccess = false;
    }

    public void Clear(SAVEFILE file)
    {
        if (file == SAVEFILE.Level)
        {
            levelStrings.Clear();
        }
        else if (file == SAVEFILE.Upgrade)
        {
            upgradeStrings.Clear();
        }
    }

    public void Add(string info, SAVEFILE type)
    {
        if (type == SAVEFILE.Level)
        {
            levelStrings.Add(info);
        }
        else if (type == SAVEFILE.Upgrade)
        {
            upgradeStrings.Add(info);
        }
    }

    public void Save()
    {
        string[] level = new string[levelStrings.Count];
        string[] upgrade = new string[upgradeStrings.Count];
        int i = 0;
        foreach (string s in levelStrings)
        {
            level[i] = s;
            i++;
        }
        i = 0;
        foreach (string s in upgradeStrings)
        {
            upgrade[i] = s;
            i++;
        }
        System.IO.File.WriteAllLines(upgradePath, upgrade);
        System.IO.File.WriteAllLines(levelPath, level);
    }

    public void Load(SAVEFILE file)
    {
        if (file == SAVEFILE.Level)
        {
            try
            {
                string[] level = System.IO.File.ReadAllLines(levelPath);
                Clear(file);
                foreach (string s in level)
                {
                    levelStrings.Add(s);
                }
                loadSuccess = true;
            }
            catch (System.Exception e) { 
				Debug.LogError (e);
                loadSuccess = false;
			}
        }
        else if (file == SAVEFILE.Upgrade)
        {
            try
            {
                string[] upgrades = System.IO.File.ReadAllLines(upgradePath);
                Clear(file);
                foreach (string s in upgrades)
                {
                    upgradeStrings.Add(s);
                }
                loadSuccess = true;
            }
            catch (System.Exception e) {
				Debug.LogError (e);
                loadSuccess = false;
			}
        }
    }

    public bool LoadSuccessful()
    {
        return loadSuccessful;
    }

    public List<string> GetInfo(SAVEFILE type)
    {
        if (type == SAVEFILE.Level)
        {
            return levelStrings;
        }
        else if (type == SAVEFILE.Upgrade)
        {
            return upgradeStrings;
        }
        return null;
    }

}
