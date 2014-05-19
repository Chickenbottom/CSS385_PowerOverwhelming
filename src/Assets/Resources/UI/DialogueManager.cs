using UnityEngine;
using System.Collections;
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

public class DialogueManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////

    public GUIText DialogueLeft;
    public GUIText DialogueRight;

    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    
    // TODO: prioritize statements
    public void SetRodelleStatment (DialogType d)
    {
        mCurPerson = kRod;
        mCurConvo = (int)d;
        mLength = 1;
        GameObject.Find ("dialogueL").GetComponent<SpriteRenderer> ().sortingOrder = 1100;
        GameObject.Find ("dialogueR").GetComponent<SpriteRenderer> ().sortingOrder = -1110;
        SetDialog ("", mDialogueBoxes [1]);
    }
    
    public void SetAdvisorStatment (DialogType d)
    {
        mCurPerson = kAdv;
        mCurConvo = (int)d;
        mLength = 1;
        GameObject.Find ("dialogueR").GetComponent<SpriteRenderer> ().sortingOrder = 1100;
        GameObject.Find ("dialogueL").GetComponent<SpriteRenderer> ().sortingOrder = -1110;
        GameObject.Find ("DialoguePortrait").GetComponent<SpriteRenderer> ().sortingOrder = -1110;
        SetDialog ("", mDialogueBoxes [0]);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////
    #region Private Variables
    #region const variables
    //which perons has the conversation
    private const int kRod = 0;
    private const int kAdv = 1;
    private const int kTotalArray = 5;
    private const float kLetterDisplayTime = .05f;
    private const string kPath = "Dialog.txt"; //path of the txt file
    #endregion

    int mCurPerson;
    int mCurConvo;
    int mLength; // used to display one letter at a time
    float mPreviousLetter = 0f; //used for keeping time for display

    private GUIText[] mDialogueBoxes;
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
    private void loadDialog (StreamReader file)
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
    
    private void printStatement ()
    {
        string line = (string)mConversations [mCurPerson] [mCurConvo] [0];
        if (mLength > line.Length) {
            mLength = -1;
            mConversations [mCurPerson] [mCurConvo].RemoveAt (0);
            mConversations [mCurPerson] [mCurConvo].Add (line);
            return;
        }
        
        SetDialog (line.Substring (0, mLength), mDialogueBoxes [mCurPerson]);
        mLength++;
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////

    void Start ()
    {
        mDialogueBoxes = new GUIText[2] { DialogueLeft, DialogueRight };

        #region initialize conversation arrays
        mConversations = new ArrayList[2][];
        mConversations [kRod] = new ArrayList[kTotalArray];
        mConversations [kAdv] = new ArrayList[kTotalArray];

        for (int i = 0; i < kTotalArray; i++) {
            mConversations [kRod] [i] = new ArrayList ();
            mConversations [kAdv] [i] = new ArrayList ();
        }
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
            loadDialog (file);
        }
        #endregion

        mLength = -1;
        mCurPerson = -1;
        mCurConvo = -1;

        SetRodelleStatment (DialogType.Tutorial);
    }

    void Update ()
    {
        if (Time.time - mPreviousLetter > kLetterDisplayTime && mLength != -1) {
            printStatement ();
            mPreviousLetter = Time.time;
        }
        if (mLength == -1) {
            SetAdvisorStatment (DialogType.Tutorial);
        }
    }
}
