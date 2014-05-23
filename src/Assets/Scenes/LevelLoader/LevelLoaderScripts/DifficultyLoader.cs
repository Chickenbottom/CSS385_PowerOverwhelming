using UnityEngine;
using System.Collections;
using System;



public class DifficultyLoader : MonoBehaviour {
	public static string mCurrentLevel{get; set;}
	public static Era mCurrentEra{get; set;}
	private static string[] mEraArray = Enum.GetNames(typeof(Era));

	public static void LoadGame(){
		if(mCurrentEra != null && mCurrentLevel != null){
			string temp = mEraArray[(int)mCurrentEra];
			Application.LoadLevel(mEraArray[(int)mCurrentEra] + mCurrentLevel);
		}
	}
}
