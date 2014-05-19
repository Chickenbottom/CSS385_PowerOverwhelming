using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum DialogueTrigger
{
    Tutorial = 0,
    TowerDestroyed = 1,
    RodelleDamaged = 2,
    TowerDamaged = 3,
    PeasantInvade = 5,
    ArcherMage = 6,
}

public enum Speaker 
{
    None,
    King,
    Advisor,
    Swordsman,
    Archer,
    Mage,
    Peasant,
}

public enum SpeakerState
{
    Normal,
    Nervous,
    Angry,
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
        
    public void TriggerDialogue(DialogueTrigger trigger)
    {
        if (mTriggers.ContainsKey(trigger))
            mDialogueQueue.Enqueue(mTriggers[trigger]);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////

    private const string kPath = "Dialog.txt"; //path of the txt file

    int mCurConvo;
    int mLength; // used to display one letter at a time
    float mPreviousLetter = 0f; //used for keeping time for display

    private Dictionary<SpeakerLocation, GUIText> mTextBoxes;
    private Dictionary<SpeakerLocation, GUIText> mNameBoxes;
    private Dictionary<Speaker, SpriteRenderer> mSpeakers;
    private Dictionary<SpeakerLocation, SpriteRenderer> mGuiLayers;
    
    private Dictionary<DialogueTrigger, Dialogue> mTriggers;
    private Queue<Dialogue> mDialogueQueue;
  
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
    }*/
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////

    Dialogue mDialogue;
    void Start ()
    {
        mDialogueQueue = new Queue<Dialogue>();
        
        mTriggers = new Dictionary<DialogueTrigger, Dialogue>();
        Dialogue dialogue;
        
        dialogue = new Dialogue();
        dialogue.AddMessage(7f, SpeakerState.Normal, Speaker.King, SpeakerLocation.Left, 
                             "Welcome, left click a tower to select it, and right click to send \nunits from that tower to a location.");
        dialogue.AddMessage(7f, SpeakerState.Normal, Speaker.Advisor, SpeakerLocation.Right, 
                             "Don't forget you can double click to force your units to go to a \nspecific location M'Lord.");
        
        mTriggers.Add(DialogueTrigger.Tutorial, dialogue);
        
        dialogue = new Dialogue();
        dialogue.AddMessage(5f, SpeakerState.Normal, Speaker.Swordsman, SpeakerLocation.Right, 
                            "Sir, it appears that the peasants have taken over a tower.");
        dialogue.AddMessage(5f, SpeakerState.Normal, Speaker.King, SpeakerLocation.Left, 
                            "Well don't just stand there. Go get some more swordsman and get it back!");
        dialogue.AddMessage(12f, SpeakerState.Normal, Speaker.Advisor, SpeakerLocation.Right, 
                            "If you send a group of units torwards the destroyed tower, they will \nattack it and you'll regain control");
        mTriggers.Add(DialogueTrigger.TowerDestroyed, dialogue);
            
        dialogue = new Dialogue();
        dialogue.AddMessage(4f, SpeakerState.Angry, Speaker.Mage, SpeakerLocation.Right, 
                            "Hey Archer, you almost hit me!");
        dialogue.AddMessage(5f, SpeakerState.Nervous, Speaker.Archer, SpeakerLocation.Left, 
                            "It's not my fault you're blocking my view. Stop wearing such a \nbig hat!");
        dialogue.AddMessage(3f, SpeakerState.Normal, Speaker.Mage, SpeakerLocation.Right, 
                            ".....");
        mTriggers.Add(DialogueTrigger.ArcherMage, dialogue);
        
        this.TriggerDialogue(DialogueTrigger.ArcherMage);
    
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

        if (DialogueLeft == null || DialogueRight == null || NameLeft == null || NameRight == null) {
            Debug.LogError ("Dialogue Manager not instantiated. Ensure all of the Unity presets are set.");
        }
        
        DialogueLeft.text = "";
        DialogueRight.text = "";
        NameLeft.text = "";
        NameRight.text = "";

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
    }

    // Resets the GUI and re-enables the relevant portions
    // Updates the dialogue text and name box
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
        case (Speaker.Swordsman):
            return "Swordsman Smith";
        default:
            return speaker.ToString();
        }
    }
    
    // Hides all of the Dialogue UI elements
    // DisplayMessage is responsible for enabling the correct ones
    private void ResetGui()
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
        // no dialogue to play
        if (mDialogue == null && mDialogueQueue.Count == 0) { 
            return;
        }
        
        // No dialgoue is currently playing, grab one from the queue
        if (mDialogue == null)
            mDialogue = mDialogueQueue.Dequeue();
        
        Message message;
        bool isValid = mDialogue.AdvanceMessage(Time.deltaTime, out message);
        
        if (isValid) {
            DisplayMessage(message);
            // CheckForHigherPriorityDialogues();
        } else { // indicate that more dialogue needs to be read
            mDialogue = null;
        }
    }
}
