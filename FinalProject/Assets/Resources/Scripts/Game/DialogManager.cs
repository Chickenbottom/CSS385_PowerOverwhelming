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
    const int kTowers = 0;
    const int kTroops = 1;
    const int kGame = 2;

    const float kLetterDisplayTime = 0.5f;

    private const string path = "dialog.txt"; //path of the txt file
    #endregion

    int cur_person;
    int cur_convo;
    int index; // used to display one letter at a time
    float mPreviousLetter = 0f; //used for keeping time for display

    #region Arrays

    ArrayList[][] Conversations;

    #endregion
    #endregion

    // Use this for initialization
    void Start()
    {
        ArrayList[][] Conversations = new ArrayList[2][];

        Conversations[kRod] = new ArrayList[3];
        Conversations[kAdv] = new ArrayList[3];

        Conversations[kRod][kTowers] = new ArrayList();
        Conversations[kRod][kTroops] = new ArrayList();
        Conversations[kRod][kGame] = new ArrayList();

        Conversations[kAdv][kTowers] = new ArrayList();
        Conversations[kAdv][kTroops] = new ArrayList();
        Conversations[kAdv][kGame] = new ArrayList();


        StreamReader mFile = null;
        try
        {
            mFile = new StreamReader(path);
        } catch (System.Exception e) {}
       
        if (mFile != null)
        {
            loadDialog(mFile);
        }

        index = -1;
        cur_person = -1;
        cur_convo = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - mPreviousLetter > kLetterDisplayTime && index != -1)
        {
            printStatment();
            mPreviousLetter = Time.time;
        }

    }

    private void loadDialog(StreamReader mFile)
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en");
        while (!mFile.EndOfStream)
        {
            string line = mFile.ReadLine();
            int rod = ci.CompareInfo.IndexOf(line, "Rodelle", System.Globalization.CompareOptions.IgnoreCase);
            int adv = ci.CompareInfo.IndexOf(line, "Adviser", System.Globalization.CompareOptions.IgnoreCase);
            bool containsRod = rod >= 0;
            bool containsAdv = adv >= 0;
            int cur_convo = -1;
            int cur_person = -1;

            if (containsAdv || containsRod)
            {

                if (containsRod)
                {
                    cur_person = kRod;
                }
                else if (containsAdv)
                {
                    cur_person = kAdv;
                }

                int tower = ci.CompareInfo.IndexOf(line, "Tower", System.Globalization.CompareOptions.IgnoreCase);
                int troops = ci.CompareInfo.IndexOf(line, "Troops", System.Globalization.CompareOptions.IgnoreCase);
                int game = ci.CompareInfo.IndexOf(line, "Game", System.Globalization.CompareOptions.IgnoreCase);

                if (tower >= 0)
                {
                    cur_convo = kTowers;
                }
                else if (troops >= 0)
                {
                    cur_convo = kTroops;
                }
                else if (game >= 0)
                {
                    cur_convo = kGame;
                }

            }
            if (cur_person != -1 && cur_convo != -1)
                Conversations[cur_person][cur_convo].Add(line);
        }
    }

    private void printStatment()
    {
        string line = (string)Conversations[cur_person][cur_convo][0];
        if (index >= line.Length)
        {
            index = -1;
            return;
        }

        index++;
        GameObject textDisplay = GameObject.Find("StatementGUIText");
        GUIText gui = textDisplay.GetComponent<GUIText>();
        gui.text = line.Substring(0, index + 1);
    }

    public void setStatment(int person, int subject)
    {
        cur_person = person;
        cur_convo = subject;
        index = 0;
    }
}
