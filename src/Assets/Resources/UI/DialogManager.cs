using UnityEngine;
using System.Collections;
using System.IO;

public class DialogManager : MonoBehaviour
{
    #region Variables
    #region const variables
    //which perons has the conversation
    const int kRod = 0;
    const int kAdv = 1;

    //the subject of the conversation
    public enum DialogType
    {
        Tutorial = 0,
        TowerDestroyed = 1,
        RodelleDamaged = 2,
        TowerDamaged = 3,
        PeasantInvade = 4
    }

    const int kTotalArray = 5;

    const float kLetterDisplayTime = .05f;

    private const string path = "Dialog.txt"; //path of the txt file
    #endregion

    int cur_person;
    int cur_convo;
    int length; // used to display one letter at a time
    float previousLetter = 0f; //used for keeping time for display
    
    public GUIText dialogueLeft;
	public GUIText dialogueRight;
    private GUIText[] dialogueBoxes;

    #region Arrays

    ArrayList[][] conversations;

    #endregion
    #endregion

    // Use this for initialization
    void Start()
    {

        dialogueBoxes = new GUIText[2] { dialogueLeft, dialogueRight };

        #region initialize conversation arrays
        conversations = new ArrayList[2][];
        conversations[kRod] = new ArrayList[kTotalArray];
        conversations[kAdv] = new ArrayList[kTotalArray];

        for (int i = 0; i < kTotalArray; i++)
        {
            conversations[kRod][i] = new ArrayList();
            conversations[kAdv][i] = new ArrayList();
        }
        #endregion

        #region check for dialog boxes
        if (dialogueLeft == null || dialogueRight == null)
        {
            Debug.LogError("Dialogue Manager not instantiated. Add GUIText for DialogueLeft and DialogueRight!");
        }
			
		dialogueLeft.text = "";
		dialogueRight.text = "";
        #endregion

        #region file read
        StreamReader file = null;
        try
        {
            file = new StreamReader(path);
        } catch (System.Exception e) {
			Debug.Log(e.ToString());
        }
        if (file != null)
        {
            loadDialog(file);
        }
        #endregion

        length = -1;
        cur_person = -1;
        cur_convo = -1;

        setRodelleStatment(DialogType.Tutorial);

    }

    private void SetDialog(string text, GUIText textBox)
    {
        textBox.text = text;
    }

    void Update()
    {
        if (Time.time - previousLetter > kLetterDisplayTime && length != -1)
        {
            printStatement();
            previousLetter = Time.time;
        }
        if (length == -1)
        {
            setAdvisorStatment(DialogType.Tutorial);
        }

    }

    // File must be in format
    /* !<PERSON> (in number format)
     * #<DIALOG TYPE> (in number format)
     * line
     * line
     */
    private void loadDialog(StreamReader file)
    {
        int person = -1;
        int dialogType = -1;
        string line = file.ReadLine();
        while (!file.EndOfStream)
        {
            if (line.StartsWith("!"))
            {
                person = int.Parse(line.Substring(1, 1));
                dialogType = int.Parse(file.ReadLine().Substring(1, 1));
            }
            else if (line.StartsWith("#"))
            {
                dialogType = int.Parse(line.Substring(1, 1));
            }
            
            line = file.ReadLine();

            while (!line.StartsWith("!") && !line.StartsWith("#"))
            {
                string line2 = line.Substring(1);
                line = file.ReadLine();
                while (line != null && !line.StartsWith("-") && !line.StartsWith("!") && !line.StartsWith("#"))
                {
                    line2 += '\n' + line;
                    line = file.ReadLine();
                }
                conversations[person][dialogType].Add(line2);
            }
            
        }
    }

    private void printStatement()
    {
        string line = (string) conversations[cur_person][cur_convo][0];
        if (length > line.Length)
        {
            length = -1;
            conversations[cur_person][cur_convo].RemoveAt(0);
            conversations[cur_person][cur_convo].Add(line);
            return;
        }

        SetDialog(line.Substring(0, length), dialogueBoxes[cur_person]);
        length++;
	}

    // TODO: prioritize statements
    #region public set statement
    public void setRodelleStatment(DialogType d)
    {
        cur_person = kRod;
        cur_convo = (int) d;
        length = 1;
        GameObject.Find("dialogueL").GetComponent<SpriteRenderer>().sortingOrder = 1100;
        GameObject.Find("dialogueR").GetComponent<SpriteRenderer>().sortingOrder = -1110;
        SetDialog("", dialogueBoxes[1]);
    }

    public void setAdvisorStatment(DialogType d)
    {
        cur_person = kAdv;
        cur_convo = (int) d;
        length = 1;
        GameObject.Find("dialogueR").GetComponent<SpriteRenderer>().sortingOrder = 1100;
        GameObject.Find("dialogueL").GetComponent<SpriteRenderer>().sortingOrder = -1110;
        GameObject.Find("DialoguePortrait").GetComponent<SpriteRenderer>().sortingOrder = -1110;
        SetDialog("", dialogueBoxes[0]);
    }
    #endregion

}
