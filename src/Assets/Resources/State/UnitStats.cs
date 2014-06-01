using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public enum UnitStat
{
    Health = 0,
    MovementSpeed,
    ChargeSpeed,
    SightRange,
    Level,
    Experience,
}

public static class EnumUtil {
    public static IEnumerable<T> GetValues<T>() {
        return (T[])Enum.GetValues(typeof(T));
    }
}

public static class UnitStats
{   
    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    
    static UnitStats ()
    {
        int numUnitTypes = Enum.GetValues(typeof(UnitType)).Length;
        int numStats = Enum.GetValues(typeof(UnitStat)).Length;
        mUnitStats = new float[numUnitTypes, numStats];      
        
        for (int i = 0; i < numUnitTypes; i ++)
            for (int j = 0; j < numStats; j++)
                mUnitStats [i, j] = 0f;
        
        int numEras = Enum.GetValues(typeof(UnitStat)).Length;
        mUnitLevels = new int[numUnitTypes, numEras];
        mUnitExperience = new int[numUnitTypes, numEras];
        
        for (int i = 0; i < numUnitTypes; i ++) {
            for (int j = 0; j < numEras; j++) {
                mUnitLevels [i, j] = 0;
                mUnitExperience [i, j] = 0;
            }
        }
        
        LoadStatsFromFile ("Data/unitstats.txt");
        LoadLevelsFromFile ("Data/unitlevels.txt");
    }
    
    public static float GetStat (UnitType unit, UnitStat stat)
    {
        Era currentEra = GameState.GameEra;
        
        if (stat == UnitStat.Level)
            return mUnitLevels[(int) unit, (int) currentEra];
        
        if (stat == UnitStat.Experience)
            return mUnitExperience[(int) unit, (int) currentEra];
                    
        return mUnitStats [(int)unit, (int)stat];
    }
    
    public static void SetStat (UnitType subject, UnitStat stat, float value)
    {
        if (stat == UnitStat.Level || stat == UnitStat.Experience)
            return;
            
        mUnitStats [(int)subject, (int)stat] = value;
    }
    
    public static void AddToExperience (UnitType subject, int value)
    {
        if (subject == UnitType.Peasant)
            return;
            
        Era currentEra = GameState.GameEra;
        
        mUnitExperience [(int)subject, (int)currentEra] += value;
        if (mUnitExperience [(int)subject, (int)currentEra] > GetExpToNextLevel(subject)) {
            IncreaseUnitLevel(subject);
        }
    }
    
    public static int GetExpToNextLevel(UnitType unit)
    {
        Era currentEra = GameState.GameEra;
        int unitLevel = (int)mUnitLevels[(int) unit, (int) currentEra];
        
        return unitLevel * 5 + 15;
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
    static int[,] mUnitLevels;
    static int[,] mUnitExperience;
    
    private static void IncreaseUnitLevel(UnitType unit)
    {
        Era currentEra = GameState.GameEra;
        
        mUnitStats[(int) unit, (int) UnitStat.Health] += 1;
        
        mUnitLevels[(int) unit, (int) currentEra] += 1;
        mUnitExperience[(int) unit, (int) currentEra] = 0;
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
                if (s == UnitStat.Level || s == UnitStat.Experience)
                    continue;
                mUnitStats [(int)unitType, (int)s] = float.Parse (values [statIndex]);
                statIndex ++;
            }
        }
        
        file.Close ();
    }  
    
    private static void LoadLevelsFromFile (string filepath)
    {
        StreamReader file = new StreamReader (filepath);
        char[] delim = { ' ', ',' };
        
        while (!file.EndOfStream) {
            string line = file.ReadLine ();
            
            string [] values = line.Split (delim, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length == 0 || values[0]== "#") // Ignore comments and blank lines
                continue;
            
            // FileFormat: "<UnitType>,<Era> <Level> <Experience>"
            UnitType unitType = EnumHelper.FromString<UnitType>(values[0]);
            Era era = EnumHelper.FromString<Era>(values[1]);
            
            mUnitLevels[(int)unitType, (int)era] = int.Parse(values[2]);
            mUnitExperience[(int)unitType, (int)era] = int.Parse(values[3]);
        }

        file.Close ();
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    
}