using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadSelector : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////
    // Inspector Presets
    ///////////////////////////////////////////////////////////////////////////////////
    public List<SquadManager> SquadManagers;
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Public
    ///////////////////////////////////////////////////////////////////////////////////
    
    public void SelectSquad(Squad selectedSquad)
    {
        foreach (SquadManager sm in SquadManagers) {
            foreach (Squad s in sm.Squads)
                s.ShowSelector(false);
        }
        
        mSelectedSquads.Clear();
        mSelectedSquads.Add(selectedSquad);
        
        mSelectedSquads[0].ShowSelector(true);
            
        mWasJustSelected = true;
    }
    
    public void SelectMultipleSquads(List<Squad> squads)
    {
        foreach (SquadManager sm in SquadManagers) {
            foreach (Squad s in sm.Squads)
                s.ShowSelector(false);
        }
        
        mSelectedSquads = squads;
        foreach (Squad s in mSelectedSquads)
            s.ShowSelector(true);
        
        mWasJustSelected = true;
    }
    
    public void PerformRightClickAction(Target target)
    {
        //mSelectedSquad.UseTargetedAbility(target);
        mWasJustSelected = true;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Private
    ///////////////////////////////////////////////////////////////////////////////////
    
    private List<Squad> mSelectedSquads;
    private Squad SquadClicked;
    private bool mWasJustSelected;
    
    private Queue<GameObject> mActiveRallyPoints;
    
    private static GameObject mRallyPointPrefab;
    
    private void DestroyRallyPoint()
    {
        GameObject o = mActiveRallyPoints.Dequeue();
        Destroy (o);
    }
    
    private void SetSquadDestination(Vector3 location)
    {
        foreach (Squad s in mSelectedSquads)
            s.SetDestination(location);
        
        GameObject o = (GameObject)Instantiate (mRallyPointPrefab);
        o.transform.position = location;
        
        mActiveRallyPoints.Enqueue(o);
        Invoke ("DestroyRallyPoint", 3f);
    }
    
    private void ForceSquadDestination(Vector3 location)
    {
        foreach (Squad s in mSelectedSquads)
            s.ForceMove(location);
        
        GameObject o = (GameObject)Instantiate (mRallyPointPrefab);
        o.transform.position = location;
        
        mActiveRallyPoints.Enqueue(o);
        Invoke ("DestroyRallyPoint", 3f);
    }
    
    ///////////////////////////////////////////////////////////////////////////////////
    // Unity Overrides
    ///////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        mSelectedSquads = new List<Squad>();
        mActiveRallyPoints = new Queue<GameObject>();
        
        if (mRallyPointPrefab == null)
            mRallyPointPrefab = Resources.Load ("Squads/RallyPointPrefab") as GameObject;
    
        if (SquadManagers == null || SquadManagers.Count == 0)
            Debug.LogError("The Squads need to be added to the Mouse Manager in the Unity Inspector.");
        
        foreach (SquadManager sm in SquadManagers) {
            foreach (Squad s in sm.Squads)
                s.ShowSelector(false);
        }
    }
    
    void LateUpdate()
    {
        if (mSelectedSquads == null || mWasJustSelected) {
            mWasJustSelected = false;
            return;
        }
        mWasJustSelected = false;
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        
        if (Input.GetMouseButtonDown(0))           
            SetSquadDestination(mousePos);
        
        if (Input.GetMouseButtonDown(1))
            ForceSquadDestination(mousePos);
    }
}