using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//the subject of the conversation
public enum DialogType
{
    Tutorial = 0,
    TowerDestroyed = 1,
    RodelleDamaged = 2,
    TowerDamaged = 3,
    PeasantInvade = 4
}

public enum Speaker 
{
    None,
    King,
    Advisor,
    Swordsman,
    Peasant,
}

public enum SpeakerState
{
    Normal,
    Nervous,
}

public enum SpeakerLocation
{
    Left,
    Right
}


public class DialogueManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////

    public GUIText DialogueLeft;
    public GUIText NameLeft;
    
    public GUIText DialogueRight;
    public GUIText NameRight;

    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    
    // TODO: prioritize statements
    
    //public void TriggerDialogue()
    /*public void SetRodelleStatment (DialogType d)
    {
        mCurrentSpeaker = Speaker.King;
        mCurConvo = (int)d;
        mLength = 1;
        GameObject.Find ("dialogueL").GetComponent<SpriteRenderer> ().sortingOrder = 1100;
        GameObject.Find ("dialogueR").GetComponent<SpriteRenderer> ().sortingOrder = -1110;
        SetDialog ("", mTextBoxes [1]);
    }
    
    public void SetAdvisorStatment (DialogType d)
    {
        mCurrentSpeaker = Speaker.Advisor;
        mCurConvo = (int)d;
        mLength = 1;
        GameObject.Find ("dialogueR").GetComponent<SpriteRenderer> ().sortingOrder = 1100;
        GameObject.Find ("dialogueL").GetComponent<SpriteRenderer> ().sortingOrder = -1110;
        GameObject.Find ("DialoguePortrait").GetComponent<SpriteRenderer> ().sortingOrder = -1110;
        SetDialog ("", mTextBoxes [0]);
    }*/

    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////
    #region Private Variables
    #region const variables
    //which perons has the conversation
    private Speaker mCurrentSpeaker;
    private const int kTotalArray = 5;
    private const float kLetterDisplayTime = .05f;
    private const string kPath = "Dialog.txt"; //path of the txt file
    #endregion

    int mCurConvo;
    int mLength; // used to display one letter at a time
    float mPreviousLetter = 0f; //used for keeping time for display

    private Dictionary<SpeakerLocation, GUIText> mTextBoxes;
    private Dictionary<SpeakerLocation, GUIText> mNameBoxes;
    private Dictionary<Speaker, SpriteRenderer> mSpeakers;
    Dictionary<SpeakerLocation, SpriteRenderer> mGuiLayers;
    private ArrayList[][] mConversations;

    #endregion
    
    
    private void SetDialog (string text, GUIText textBox)
    {
        textBox.text = text;
    }

    // File must be in format
    /* !<PERSON> (in number format)
     * #<DIALOG TYPE> (in number format)
     * line
     * line
     */
    /*private void LoadDialog (StreamReader file)
    {
        int person = -1;
        int dialogType = -1;
        string line = file.ReadLine ();
        while (!file.EndOfStream) {
            if (line.StartsWith ("!")) {
                person = int.Parse (line.Substring (1, 1));
                dialogType = int.Parse (file.ReadLine ().Substring (1, 1));
            } else if (line.StartsWith ("#")) {
                dialogType = int.Parse (line.Substring (1, 1));
            }
            
            line = file.ReadLine ();
            
            while (!line.StartsWith("!") && !line.StartsWith("#")) {
                string line2 = line.Substring (1);
                line = file.ReadLine ();
                while (line != null && !line.StartsWith("-") && !line.StartsWith("!") && !line.StartsWith("#")) {
                    line2 += '\n' + line;
                    line = file.ReadLine ();
                }
                mConversations [person] [dialogType].Add (line2);
            }
            
        }
    }
    
    private void PrintStatement ()
    {
        string line = (string)mConversations [mCurrentSpeaker] [mCurConvo] [0];
        if (mLength > line.Length) {
            mLength = -1;
            mConversations [mCurrentSpeaker] [mCurConvo].RemoveAt (0);
            mConversations [mCurrentSpeaker] [mCurConvo].Add (line);
            return;
        }
        
        SetDialog (line.Substring (0, mLength), mDialogueBoxes [mCurrentSpeaker]);
        mLength++;
    }*/

    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////

    Dialogue mDialogue;
    void Start ()
    {
        mDialogue = new Dialogue();
        mDialogue.AddMessage(7f, SpeakerState.Normal, Speaker.King, SpeakerLocation.Left, 
                             "Welcome, left click a tower to select it, and right click to send \nunits from that tower to a location.");
        mDialogue.AddMessage(10f, SpeakerState.Normal, Speaker.Advisor, SpeakerLocation.Right, 
                             "Don't forget you can double click to force your units to go to a \nspecific location M'Lord.");
    
        mTextBoxes = new Dictionary<SpeakerLocation, GUIText>() ;
        mTextBoxes.Add (SpeakerLocation.Left, DialogueLeft);
        mTextBoxes.Add (SpeakerLocation.Right, DialogueRight);
        
        mNameBoxes = new Dictionary<SpeakerLocation, GUIText>() ;
        mNameBoxes.Add (SpeakerLocation.Left, NameLeft);
        mNameBoxes.Add (SpeakerLocation.Right, NameRight);
        
        mGuiLayers = new Dictionary<SpeakerLocation, SpriteRenderer>();
        mGuiLayers.Add (SpeakerLocation.Left, GameObject.Find ("ChatImageLeft").GetComponent<SpriteRenderer>());
        mGuiLayers.Add (SpeakerLocation.Right, GameObject.Find ("ChatImageRight").GetComponent<SpriteRenderer>());
        
        mSpeakers = new Dictionary<Speaker, SpriteRenderer>();
        mSpeakers.Add (Speaker.King, GameObject.Find ("DialoguePortrait").GetComponent<SpriteRenderer>());

        #region initialize conversation arrays
        mConversations = new ArrayList[2][];
        /*mConversations [kRod] = new ArrayList[kTotalArray];
        mConversations [kAdv] = new ArrayList[kTotalArray];

        for (int i = 0; i < kTotalArray; i++) {
            mConversations [kRod] [i] = new ArrayList ();
            mConversations [kAdv] [i] = new ArrayList ();
        }*/
        #endregion

        #region check for dialog boxes
        if (DialogueLeft == null || DialogueRight == null) {
            Debug.LogError ("Dialogue Manager not instantiated. Add GUIText for DialogueLeft and DialogueRight!");
        }
            
        DialogueLeft.text = "";
        DialogueRight.text = "";
        #endregion

        #region file read
        StreamReader file = null;
        try {
            file = new StreamReader (kPath);
        } catch (System.Exception e) {
            Debug.Log (e.ToString ());
        }
        if (file != null) {
           // LoadDialog (file);
        }
        #endregion

        mLength = -1;
        mCurrentSpeaker = Speaker.None;
        mCurConvo = -1;

        //SetRodelleStatment (DialogType.Tutorial);
    }

    private void DisplayMessage(Message message)
    {
        ResetGui();
        
        Speaker speaker = message.Who;
        if (mSpeakers.ContainsKey(speaker)) // TODO add speaker state change here
            mSpeakers[speaker].enabled = true;
        
        SpeakerLocation location = message.Location;
        
        mTextBoxes[location].enabled = true;
        mTextBoxes[location].text = message.Text;
        
        mNameBoxes[location].enabled = true;
        mNameBoxes[location].text = MapSpeakerToName(message.Who);
        
        mGuiLayers[location].enabled = true;
    }
    
    private string MapSpeakerToName(Speaker speaker)
    {
        switch(speaker){
        case (Speaker.King):
            return "King Rodelle";
        default:
            return speaker.ToString();
        }
    }
    
    void ResetGui()
    {
        foreach (Speaker s in mSpeakers.Keys)
            mSpeakers[s].enabled = false;
            
        foreach (SpeakerLocation l in mTextBoxes.Keys)
            mTextBoxes[l].enabled = false;
            
        foreach (SpeakerLocation l in mNameBoxes.Keys)
            mNameBoxes[l].enabled = false;
             
        foreach (SpeakerLocation l in mGuiLayers.Keys)
            mGuiLayers[l].enabled = false;
    }
    

    void Update ()
    {
        Message message;
        bool isValid = mDialogue.AdvanceMessage(Time.deltaTime, out message);
        
        if (isValid) {
            DisplayMessage(message);
            // CheckForAdditionalDialogue();
        }
    }
}
