using UnityEngine;
using System.Collections;
using System.IO;

public class DialogManager : MonoBehaviour {
	#region Variables
	#region const variables
	//which perons has the conversation
	const int kRod = 0;
	const int kAdv = 1;

	//the subject of the conversation
	const int kTowers = 0;
	const int kTroops = 1;
	const int kGame = 2;

	const float kLetterDisplayTime = 0.5f; 
	#endregion

	int cur_person = -1; 
	int cur_convo = -1;	
	int index = 0; // used to display one letter at a time
	float mPreviousLetter = 0f; //used for keeping time for display

	private const string path = "dialog.txt"; //path of the txt file
	StreamReader mFile; 
	string line; //used to read line from mfile and arrays
	string displayStatment; //string that builds full statment over time

	#region Arrays

	ArrayList[][] Conversations = new ArrayList[2][3]; 
	ArrayList RodTowerStatments;
	ArrayList AdvTowerStatments;
	ArrayList RodTroopStatments;
	ArrayList AdvTroopStatments;
	ArrayList RodGameStatments;
	ArrayList AdvGameStatments;

	#endregion
	#endregion
	// Use this for initialization
	void Start () {
		mFile = new StreamReader(path);

		Conversations[kRod][kTowers].Add(RodTowerStatments);
		Conversations[kRod][kTroops].Add(RodTowerStatments);
		Conversations[kRod][kGame].Add(RodTowerStatments);

		Conversations[kAdv][kTowers].Add(RodTowerStatments);
		Conversations[kAdv][kTroops].Add(RodTowerStatments);
		Conversations[kAdv][kGame].Add(RodTowerStatments);
		

		loadDialog();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.realtimeSinceStartup - mPreviousLetter > kLetterDisplayTime ){
			printStatment();
			mPreviousLetter = Time.realtimeSinceStartup;
			
		}
			
	}
	void loadDialog(){
		string[] substring;
		while(!mFile.EndOfStream){
			line = mFile.ReadLine();
			if(line.Contains("Rodelle", System.StringComparison.CurrentCultureIgnoreCase) ||
			   line.Contains("Adviser", System.StringComparison.CurrentCultureIgnoreCase)){

				if(line.Contains("Rodelle", System.StringComparison.CurrentCultureIgnoreCase)){
					cur_person = kRod;
				}
				else if(line.Contains("Adviser", System.StringComparison.CurrentCultureIgnoreCase)){
					cur_person = kAdv;
				}
				if(line.Contains("Towers", System.StringComparison.CurrentCultureIgnoreCase)){
					cur_convo = kTowers;
				}
				else if(line.Contains("Troops", System.StringComparison.CurrentCultureIgnoreCase)){
					cur_convo = kTroops;
				}
				else if(line.Contains("Game", System.StringComparison.CurrentCultureIgnoreCase)){
					cur_convo = kGame
				}
			}
			if(cur_person != -1 && cur_convo != -1)
				Conversations[cur_person][cur_convo].Add(line);
		}
	}
	void printStatment(){
	   if(index >= line.Length)
			return;

		displayStatment += line[index];
		index++;

		GameObject textDisplay = GameObject.Find("StatementGUIText");
		GUIText gui = textDisplay.GetComponent<GUIText>();
		gui.text = displayStatment;

	}
	public void setStatment(int person, int subject){
		line = Conversations[person][subject];
		index = 0;
	}
}
