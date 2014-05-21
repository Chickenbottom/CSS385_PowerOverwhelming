using UnityEngine;
using System.Collections;
using System.IO;
using System;

public enum BonusSubject : int {
	Melee = 0,
	Ranged = 1,
	Magic = 2,
	Ability = 3,
}

public enum BonusType : int{
	Health = 0,
	CoolDown = 1,
	SpawnSize = 2,
	SpawnRate = 3,
	AmpAbility = 4,
}


public class Upgrades : MonoBehaviour
{

	#region Variables
	#region const variables

	const float kLetterDisplayTime = 0.5f;
	#endregion

    //string[] subjectName = ;
	int mSubjectMax = Enum.GetNames(typeof(BonusSubject)).Length;
	int mBonusMax = Enum.GetNames(typeof(BonusType)).Length;

	
	//int mCurSubject = -1;
	//int mCurBonus = -1;

	private const string path = "Bonus.txt"; //path of the txt file
	StreamReader mFile;
	string line; //used to read line from mfile and arrays
	
	
	float[,] mBonusArray;

	#endregion
	
	// Use this for initialization
	void Start()
	{
		mBonusArray = new float[mSubjectMax, mBonusMax];
		
		if(File.Exists(path)){
			LoadBonuses();		
		}
		else{
			PopulateArray();
			WriteBonuses();
		}
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
	
	void LoadBonuses()
	{
		mFile = new StreamReader(path);	
		System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en");
		char[] delim = new char[1];
		delim[0] = ',';
		while (!mFile.EndOfStream)
		{
			int curSubject = -1;
			int curBonus = -1;
			line = mFile.ReadLine();

			string []numbers = line.Split(delim[0]);

			if(numbers.Length < 3)
				continue;


			int ability = ci.CompareInfo.IndexOf(numbers[0], "Ability", System.Globalization.CompareOptions.IgnoreCase);
			int melee = ci.CompareInfo.IndexOf(numbers[0], "Melee", System.Globalization.CompareOptions.IgnoreCase);
			int ranged = ci.CompareInfo.IndexOf(numbers[0], "Ranged", System.Globalization.CompareOptions.IgnoreCase);
			int magic = ci.CompareInfo.IndexOf(numbers[0], "Special", System.Globalization.CompareOptions.IgnoreCase);

			int health = ci.CompareInfo.IndexOf(numbers[1], "Health", System.Globalization.CompareOptions.IgnoreCase);
			int spawnRate = ci.CompareInfo.IndexOf(numbers[1], "CoolDown", System.Globalization.CompareOptions.IgnoreCase);
			int spawnSize = ci.CompareInfo.IndexOf(numbers[1], "SpawnSize", System.Globalization.CompareOptions.IgnoreCase);
			int ampAbiilty = ci.CompareInfo.IndexOf(numbers[1], "AmpAbility", System.Globalization.CompareOptions.IgnoreCase);
			

			if(ability >= 0)
				curSubject = (int)BonusSubject.Ability;
			else if(melee >= 0)
				curSubject = (int)BonusSubject.Melee;
			else if(ranged >= 0)
				curSubject = (int)BonusSubject.Ranged;
			else if(magic >= 0)
				curSubject = (int)BonusSubject.Magic;

			if(health >= 0)
				curBonus = (int)BonusType.Health;
			else if(spawnRate >= 0)
				curBonus = (int)BonusType.SpawnRate;
			else if(ampAbiilty >= 0)
				curBonus = (int)BonusType.AmpAbility;
			else if(spawnSize >= 0)
				curBonus = (int)BonusType.SpawnSize;
		

			if (curSubject != -1 && curBonus != -1){
				mBonusArray[curSubject, curBonus] = float.Parse(numbers[2]);				
			}
		}
		mFile.Close();
	}	
	float GetBonus(BonusSubject subject, BonusType bonus){
		return mBonusArray[(int)subject ,(int) bonus];
	}
	public void SetBonus(BonusSubject subject, BonusType bonus, float value){
		mBonusArray[(int)subject ,(int) bonus] = value;
	}
	public void WriteBonuses(){
		StreamWriter writer = new StreamWriter(path);

			foreach(string sub in Enum.GetNames(typeof(BonusSubject))){
				foreach(string bon in Enum.GetNames(typeof(BonusType))){
					writer.WriteLine( sub + "," + bon + "," +
                	mBonusArray[(int)Enum.Parse(typeof(BonusSubject),sub) , 
				            (int)Enum.Parse(typeof(BonusType),bon)].ToString());
				}
		}
		writer.Close();
	}
	void PopulateArray(){
		for(int i = 0; i < mSubjectMax; i ++)
			for(int j = 0; j < mBonusMax; j++)
				mBonusArray[i ,j] = 1f;
	}
	public float GetUnitUpgrades(UnitType unit, BonusType sType){
		BonusSubject mySubject = (BonusSubject)(-1);
		BonusType myType = sType;

		switch(unit){
		case UnitType.Swordsman:
			mySubject = BonusSubject.Melee;
			break;
		case UnitType.Archer:
			mySubject = BonusSubject.Ranged;
			break;
		case UnitType.Mage:
			mySubject = BonusSubject.Magic;
			break;

		}
		if(mySubject != (BonusSubject)(-1))
			return GetBonus(mySubject, myType);
		else
			return 1.0f;
	}
	public void SetTowerArray(ref float[,] tempArray){
		for(int i = 0; i < mSubjectMax; i ++)
			for(int j= 0; j < mBonusMax; j++){
				tempArray[i,j] = mBonusArray[i,j]; 
		}
	}
	public void SetUpgradeArray(float[,] tempArray){
		for(int i = 0; i < mSubjectMax; i ++)
			for(int j= 0; j < mBonusMax; j++){
				mBonusArray[i,j] = tempArray[i,j]; 
		}
	}

}