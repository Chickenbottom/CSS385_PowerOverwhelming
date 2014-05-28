using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ShaddySeamusDialogue : MonoBehaviour
{

	public GUIText mDialogueText;
		
	
	const string path = "Data/ShaddySeamusDialogue.txt"; //path of the txt file
	StreamReader mFile;
	string line; //used to read line from mfile and arrays

	int index = 0;
	bool isNegitive = false;
	bool isPositive = false;
	bool isFinishedSpeaking = false;

	const float kLetterDisplayTime = .05f;
	float mLastLetter = 0f;

	ArrayList PosDialogue = new ArrayList();
	ArrayList NegDialogue = new ArrayList();
		
	// Use this for initialization
	void Start()
	{
		if(File.Exists(path))
			LoadDialogue();				
		else
			Debug.LogError("Could not find Shaddy Seamus's Dialogue.");
		line = "Welcome to my Galatic Tower Store. Here you can find what you need to put your Kingdom in order m`Lord.";
	}
	void Update(){
		if(!isFinishedSpeaking)
			if(Time.time - mLastLetter > kLetterDisplayTime){		
				WriteLetter();
				mLastLetter = Time.time;
			}
	}
	void LoadDialogue()
	{
		mFile = new StreamReader(path);	
		System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en");
		while (!mFile.EndOfStream)
		{
			line = mFile.ReadLine();

			int pos = ci.CompareInfo.IndexOf(line, "Positive", System.Globalization.CompareOptions.IgnoreCase);
			int neg = ci.CompareInfo.IndexOf(line, "Negative", System.Globalization.CompareOptions.IgnoreCase);

			
			if(pos >= 0){
				isPositive = true;
				isNegitive = false;
				continue;
			}
			else if(neg >= 0){
				isNegitive = true;
				isPositive = false;
				continue;
			}

			if(isPositive){
				PosDialogue.Add(line);
			}
			else if(isNegitive){
				NegDialogue.Add(line);
			}
		}
		mFile.Close();
	}	
	public void WriteNegDialogue(){
		if(NegDialogue.Count <=  0)
			return;
		if(isFinishedSpeaking){
			line = NegDialogue[UnityEngine.Random.Range(0,NegDialogue.Count)].ToString();
			isFinishedSpeaking = false;
			mDialogueText.text = "";	
		}
	}
	public void WritePosDialogue(){
		if(PosDialogue.Count <=  0)
			return;
		if(isFinishedSpeaking){
			line = PosDialogue[UnityEngine.Random.Range(0,PosDialogue.Count)].ToString();
			isFinishedSpeaking = false;
			mDialogueText.text = "";	
		}
	}
			
	void WriteLetter(){
		if(index >= line.Length){
			index = 0;
			isFinishedSpeaking = true;
			return;
		}
		if(index % 70 == 0)
			mDialogueText.text += "\n"; 
			
		mDialogueText.text += line[index]; 
		index++;
	
	}
		
}
