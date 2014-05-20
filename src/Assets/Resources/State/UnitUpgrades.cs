using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public enum UnitStat
{
    Level = 0,
    Experience,
    Health,
    MovementSpeed,
    ChargeSpeed,
    SightRange,
}

public static class EnumUtil {
    public static IEnumerable<T> GetValues<T>() {
        return (T[])Enum.GetValues(typeof(T));
    }
}

public static class UnitUpgrades
{   
    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    
    static UnitUpgrades ()
    {
        int numUnitTypes = Enum.GetValues(typeof(UnitType)).Length;
        int numStats = Enum.GetValues(typeof(UnitStat)).Length;
        mUnitStats = new float[numUnitTypes, numStats];      
        
        for (int i = 0; i < numUnitTypes; i ++)
            for (int j = 0; j < numStats; j++)
                mUnitStats [i, j] = 0f;
                
        LoadStatsFromFile ("unitstats.txt");
    }
    
    public static float GetStat (UnitType unit, UnitStat stat)
    {
        return mUnitStats [(int)unit, (int)stat];
    }
    
    public static void SetStat (UnitType subject, UnitStat stat, float value)
    {
        mUnitStats [(int)subject, (int)stat] = value;
    }
    
    public static void AddToExperience (UnitType subject, float value)
    {
        mUnitStats [(int)subject, (int)UnitStat.Experience] += value;
        if (mUnitStats [(int)subject, (int)UnitStat.Experience] > GetExpToNextLevel(subject)) {
            IncreaseUnitLevel(subject);
        }
    }
    
    public static int GetExpToNextLevel(UnitType unit)
    {
        return (int)mUnitStats[(int) unit, (int) UnitStat.Level] * 5 + 15;
    }
    
    /*
    public static void WriteStats ()
    {
        StreamWriter writer = new StreamWriter (filepath);
        
        foreach (string sub in Enum.GetNames(typeof(GameType))) {
            foreach (string bon in Enum.GetNames(typeof(UnitStat))) {
                writer.WriteLine (sub + "," + bon + "," +
                                  mstatArray [(int)Enum.Parse (typeof(GameType), sub), 
                             (int)Enum.Parse (typeof(UnitStat), bon)].ToString ());
            }
        }
        writer.Close ();
    }*/
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////
    
    static float[,] mUnitStats;
    
    private static void IncreaseUnitLevel(UnitType unit)
    {
        mUnitStats[(int) unit, (int) UnitStat.Level] += 1;
        mUnitStats[(int) unit, (int) UnitStat.Health] += 1;
        mUnitStats[(int) unit, (int) UnitStat.Experience] = 0;
    }
    
    private static void LoadStatsFromFile (string filepath)
    {
        StreamReader file = new StreamReader (filepath);
        char[] delim = { ' ', ',' };
        
        while (!file.EndOfStream) {
            string line = file.ReadLine ();
            
            string [] values = line.Split (delim, StringSplitOptions.RemoveEmptyEntries);
            if (values[0]== "#")
                continue;
                
            UnitType unitType = EnumHelper.FromString<UnitType>(values[0]);
            
            int statIndex = 1;
            foreach (UnitStat s in EnumUtil.GetValues<UnitStat>()) {
                mUnitStats [(int)unitType, (int)s] = float.Parse (values [statIndex]);
                statIndex ++;
            }
        }
        file.Close ();
    }  
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    
}