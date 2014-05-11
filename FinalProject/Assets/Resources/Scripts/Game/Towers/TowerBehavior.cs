using UnityEngine;
using System.Collections;

public abstract class TowerBehavior : MonoBehaviour {

    ////abstract void Click();
    ////abstract void getMyTowerType();

    //public enum TOWERTYPE{
    //    MELEE,
    //    RANGED,
    //    MAGIC,
    //    HEAL,
    //    NONE,
    //};

    //AbilityTowerBehavior abilityTower = null;
    //UnitSpawnTower spawnTower = null;
    //TOWERTYPE selectedTower;
    //// Use this for initialization
    //void Start () {
    //    selectedTower = TOWERTYPE.NONE;
    //}
	
    //// Update is called once per frame
    //void Update () {

    //    if(Input.GetMouseButtonUp(1) ){
    //        if(selectedTower != TOWERTYPE.NONE && 
    //           selectedTower != TOWERTYPE.HEAL && 
    //           spawnTower != null){
    //            Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //            spawnTower.setDestination(destination);
    //            clearSelected();
    //        }
    //    }

    //    if(Input.GetKey(KeyCode.A)){
		
    //        if(spawnTower != null){
    //            spawnTower.hitTower(50f);
    //        }
    //        if(abilityTower != null){
    //            abilityTower.hitTower(50f);
    //        }
    //    }
    //}
    //public void setSelected(UnitSpawnTower ST = null, AbilityTowerBehavior AT = null){

		
    //    if(selectedTower == TOWERTYPE.HEAL)
    //        if(ST != null){
    //            spawnTower = ST;
    //            spawnTower.healTower(abilityTower.getHealRate());
    //            clearSelected();
    //    }

    //    clearSelected();
    //    if(selectedTower == TOWERTYPE.NONE){
    //        if(AT == null){
    //            spawnTower = ST;
    //            selectedTower = ST.getMyTowerType();
    //        }
    //        else{
    //            abilityTower = AT;
    //            selectedTower = AT.getMyTowerType();
    //        }
    //    }
    //}
    //public void clearSelected(){
    //    selectedTower = TOWERTYPE.NONE;
    //    abilityTower = null;
    //    spawnTower = null;
    //}

    abstract void Click();
    abstract void GetTowerType();
    abstract void Start();

    void Start()
    {

    }

    void Update()
    {

    }

}
