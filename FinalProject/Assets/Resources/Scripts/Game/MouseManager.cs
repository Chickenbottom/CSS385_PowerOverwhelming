using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour
{

    #region variables
    private GameObject mTowerSelected;
    private bool mSelected;
    #endregion


    void Start()
    {
        mSelected = false;
        mTowerSelected = null;
    }

    void Update()
    {
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
        // Add a dead spot around the towers to prevent sending units when you meant to reselect a tower
        return mousePos.x < 240f && mousePos.x > -250f && mousePos.y < 102f && mousePos.y > -169f;
    }

    public void Select(GameObject tower)
    {
        mTowerSelected = tower;
        mSelected = true;
    }

}
