using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

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
    
    // Queues the related dialogue for play
    // TODO add priority
    public void TriggerDialogue(string trigger)
    {
        if (mTriggers.ContainsKey(trigger)) {
            mDialogueQueue.Enqueue(mTriggers[trigger]);
            mTriggers.Remove(trigger); // only plays once
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////

    private Dictionary<SpeakerLocation, GUIText> mTextBoxes;
    private Dictionary<SpeakerLocation, GUIText> mNameBoxes;
    private Dictionary<Speaker, SpriteRenderer> mSpeakers;
    private Dictionary<SpeakerLocation, SpriteRenderer> mGuiLayers;
    
    private Dictionary<string, Dialogue> mTriggers;
    private Queue<Dialogue> mDialogueQueue;
  
    // See src/dialogue_1.txt for formatting the file
    // TODO load the dialogue file for the current level instead of hardcoding dialogue_1.txt
    private void LoadDialogueFromFile (string filepath)
    {
        StreamReader file = new StreamReader (filepath);
        char[] delim = { ' ', ',' };
        
        while (!file.EndOfStream) {
            string line = file.ReadLine ();
            
            //string [] values = line.Split (delim, StringSplitOptions.RemoveEmptyEntries);
            string [] values = line.Split(delim, 5, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length == 0 || values[0]== "#") // ignore blank lines and comments
                continue;
            
            if (values[0].Contains(">>>")) { // start trigger
                string trigger = values[1];
                
                Dialogue dialogue = GetMessagesFromFile(file);
                mTriggers.Add(trigger, dialogue);
            }
        }
        
        file.Close ();
    }  
    
    private Dialogue GetMessagesFromFile(StreamReader file)
    {
        Dialogue dialogue = new Dialogue();
        
        char[] delim = { ' ', ',' };
        
        while (true) {
            string line = file.ReadLine ();
            string [] values = line.Split(delim, 6, StringSplitOptions.RemoveEmptyEntries);
            
            if (values[0].Contains ("<<<")) // end trigger
                break;
            
            // read the message
            float duration = float.Parse(values[0]);
            SpeakerState state = EnumHelper.FromString<SpeakerState>(values[1]);
            Speaker speaker = EnumHelper.FromString<Speaker>(values[2]);
            SpeakerLocation location = EnumHelper.FromString<SpeakerLocation>(values[3]);
            // ignore the literal "---"
            string message = values[5];
            
            dialogue.AddMessage(duration, state, speaker, location, message);
        }
        
        return dialogue;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////

    Dialogue mDialogue;
    void Start ()
    {
        mDialogueQueue = new Queue<Dialogue>();
        mTriggers = new Dictionary<string, Dialogue>();
    
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
        
        LoadDialogueFromFile("Data/dialogue_1.txt");
        this.TriggerDialogue("Tutorial");
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
            //ResetGui();
            mDialogue = null;
        }
    }
}
