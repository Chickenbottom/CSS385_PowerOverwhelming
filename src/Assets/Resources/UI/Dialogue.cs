using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// who is saying what, and where
public struct Message 
{
    public float Duration;
    public Speaker Who;
    public string Text;
    public SpeakerLocation Location;
    public SpeakerState State;
}

public class Dialogue
{   
    private const float kLetterDisplayTime = .05f;
    public bool IsDone = false;
    
    Queue<Message> mMessages;
    
    public Dialogue()
    {
        mMessages = new Queue<Message>();
        
        // dummy object that is removed on the first call to AdvanceMessage
        mMessages.Enqueue (new Message()); 
        mDialogueTimer = 0;
    }
    
    /// <summary>
    /// Advances the dialogue
    /// </summary>
    /// <returns><c>false</c>, if there are no more messages in the dialogue, 
    /// <c>true</c>, if the message was successfully updated.</returns>
    /// <param name="deltaTime">Delta time. The time since the last AdvanceMessage call.</param>
    /// <param name="message">Message. Returned containing information on how to display the message.</param>
    public bool AdvanceMessage(float deltaTime, out Message message)
    {
        if (IsDone) {
            message = new Message();
            return false;
        }
        
        mDialogueTimer += deltaTime;
        
        // The current message is finished, advance to the next one
        if (mDialogueTimer > mTimeUntilNextMessage) { 
            mMessages.Dequeue(); // the current message is finished
            
            if (mMessages.Count > 0) {
                SetupNextMessage();
            } else {
                IsDone = true;
                message = new Message();
                return false;
            }
        }
        
        AdvanceLetterAnimation();
        message = mCurrentMessage;
        return true;
    }
    
    private Message mCurrentMessage; // information about who the current speaker and what is being said
    private float mDialogueTimer; // the total duration this dialogue has been running
    private float mTimeUntilNextMessage = 0; // the duration of the current message
    private string mCurrentText; // the current text being manipulated
    private int mStringLength; // used to display the message one character at a time
    private float mPreviousLetterTime;
    
    private void SetupNextMessage()
    {
        mCurrentMessage = mMessages.Peek();
        mTimeUntilNextMessage = mCurrentMessage.Duration;
        mCurrentText = mCurrentMessage.Text;
        mDialogueTimer = 0;
        mStringLength = 1;
    }
    
    private void AdvanceLetterAnimation()
    {
        if (mStringLength >= mCurrentText.Length || Time.time - mPreviousLetterTime < kLetterDisplayTime)        
            return;
        
        mPreviousLetterTime = Time.time;
        mStringLength++;
        
        // TODO only add newlines between words, get rid of extra space after newline
        // TODO dynamically calculate this width based on the font?
        if (mStringLength % 60 == 0) { // add a newline every 60 characters
            string modifiedText = mCurrentText.Substring(0, mStringLength);
            modifiedText += "\n";
            mCurrentText = modifiedText + 
                mCurrentText.Substring(mStringLength, mCurrentText.Length - mStringLength);            
        }
        
        mCurrentMessage.Text = mCurrentText.Substring(0, mStringLength);
    }
    
    // Returns false to indicate that there is no valid message
    private bool EmptyDialogue(out Message message)
    {
        IsDone = true;
        message = new Message();
        return false;
    }
    
    // when, how, who, where, what
    // ex. for <5> seconds, <Nervous> <King> (<Left> side), says <"Hello world!">, 
    //     for <3.2> seconds, <Normal> <Peasant> (<Right> side), says <"....blargh.">
    public void AddMessage(float duration, SpeakerState state, Speaker speaker, SpeakerLocation location, string text)
    {
        Message message = new Message();
        message.Duration = duration;
        message.Who = speaker;
        message.Text = text;
        message.Location = location;
        message.State = state;
        
        mMessages.Enqueue (message);
    }
}
