using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class DialogueManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////
    
    public List<SpeakerLocation> SpeakerLocations;
    public List<GUIText> NameTextBoxes;
    public List<GUIText> DialogueTextBoxes;
    
    public List<Speaker> Speakers;
    
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
    
    public void TriggerChatter()
    {
    
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////

    private Dictionary<SpeakerLocation, GUIText> mTextBoxes;
    private Dictionary<SpeakerLocation, GUIText> mNameBoxes;

    private Dictionary<string, Speaker> mSpeakers;
    
    private Dictionary<string, Dialogue> mTriggers;
    private Queue<Dialogue> mDialogueQueue;
    
    private Dialogue mDialogue;
  
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
            string speaker = values[2];
            SpeakerLocation location = EnumHelper.FromString<SpeakerLocation>(values[3]);
            // ignore the literal "---"
            string message = values[5];
            
            dialogue.AddMessage(duration, state, speaker, location, message);
        }
        
        return dialogue;
    }
    
    // Resets the GUI and re-enables the relevant portions
    // Updates the dialogue text and name box
    private void DisplayMessage(Message message)
    {
        ResetGui();
        
        string speakerKey = message.Who + message.Location.ToString();
        
        Speaker speaker;
        if (! mSpeakers.ContainsKey(speakerKey))
            speaker = mSpeakers["None" + message.Location.ToString()];
        else 
            speaker = mSpeakers[speakerKey];
            
        speaker.Activate(message.State);
        
        SpeakerLocation location = message.Location;
        
        mTextBoxes[location].enabled = true;
        mTextBoxes[location].text = message.Text;
        
        mNameBoxes[location].enabled = true;
        mNameBoxes[location].text = speaker.DisplayedName;
    }
    
    // Hides all of the Dialogue UI elements
    // DisplayMessage is responsible for enabling the correct ones
    private void ResetGui()
    {            
        foreach (SpeakerLocation l in mTextBoxes.Keys)
            mTextBoxes[l].enabled = false;
        
        foreach (SpeakerLocation l in mNameBoxes.Keys)
            mNameBoxes[l].enabled = false;
        
        foreach (string s in mSpeakers.Keys)
            mSpeakers[s].Deactivate();
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////

    void Start ()
    {
        mDialogueQueue = new Queue<Dialogue>();
        mTriggers = new Dictionary<string, Dialogue>();
    
        mTextBoxes = new Dictionary<SpeakerLocation, GUIText>();
        mNameBoxes = new Dictionary<SpeakerLocation, GUIText>();
        
        for (int i = 0; i < SpeakerLocations.Count; ++i) {
            SpeakerLocation location = SpeakerLocations[i];
            
            mTextBoxes.Add (location, DialogueTextBoxes[i]);
            mTextBoxes[location].text = "";
            
            mNameBoxes.Add (location, NameTextBoxes[i]);
            mNameBoxes[location].text = "";
        }
        
        mSpeakers = new Dictionary<string, Speaker>();
        
        for (int i = 0; i < Speakers.Count; ++i) {
            string speakerKey = Speakers[i].SpeakerName + Speakers[i].Location.ToString();
            mSpeakers.Add (speakerKey, Speakers[i]);
        }
        
        LoadDialogueFromFile("Data/dialogue_1.txt");
        this.TriggerDialogue("ArcherMage");
        this.TriggerDialogue("Tutorial");
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
