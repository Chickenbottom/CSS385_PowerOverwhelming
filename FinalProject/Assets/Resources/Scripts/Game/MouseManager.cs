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
        if (Input.GetMouseButtonDown(0) && mSelected)
        {
            Debug.Log("HEYO");
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
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
