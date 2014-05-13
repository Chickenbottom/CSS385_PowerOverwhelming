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
        if (Input.GetMouseButtonDown(1) && mSelected)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            if ((TowerBehavior) mTowerSelected.ValidMousePos(mousePos))
            {
                mTowerSelected.GetComponent<TowerBehavior>().Click();
                mTowerSelected = null;
                mSelected = false;
            }
        }
    }

    public void Select(GameObject tower)
    {
        mTowerSelected = tower;
        mSelected = true;
    }

}
