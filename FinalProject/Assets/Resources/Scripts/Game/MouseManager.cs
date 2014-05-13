using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour
{

    #region variables
    private GameObject mTowerSelected;
    private bool mSelected;
    //private Bounds mClickBox;
    #endregion

    

    void Start()
    {
        mSelected = false;
        mTowerSelected = null;
        //BoxCollider2D temp = GameObject.Find("ClickBox").GetComponent<BoxCollider2D>();
        //mClickBox = new Bounds(new Vector3(temp.center.x, temp.center.y, 0f), new Vector3(temp.size.x, temp.size.y, 0f));
        //Debug.Log(mClickBox);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && mSelected)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            //if (ValidMousePos(mousePos))
            {
                mTowerSelected.GetComponent<TowerBehavior>().Click();
                mTowerSelected = null;
                mSelected = false;
            }
        }
    }

    //private bool ValidMousePos(Vector3 mousePos)
    //{
    //    //return mClickBox.Contains(mousePos);
    //}

    public void Select(GameObject tower)
    {
        mTowerSelected = tower;
        mSelected = true;
    }

}
