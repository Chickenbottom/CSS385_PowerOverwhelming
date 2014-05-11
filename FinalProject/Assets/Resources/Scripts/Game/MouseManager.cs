using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour
{

    #region variables
    private GameObject mTowerSelected;
    private bool mSelected;
    #endregion


    void Start () {
        mSelected = false;
        mTowerSelected = null;
	}
	
	void Update () {
        Debug.Log(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && mSelected)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            if (ValidMousePos(mousePos))
            {
                // mItemSelected.Click(mousePos);
                mTowerSelected = null;
                mSelected = false;
            }
        }
	}

    private bool ValidMousePos(Vector3 mousePos)
    {
        // click inside game boundry
        return true;
    }

    public void Select(GameObject tower)
    {
        mTowerSelected = tower;
        mSelected = true;
    }

}
