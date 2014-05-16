using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {

    public abstract bool ValidMousePos(Vector3 mousePos);
    public abstract void UseAbility(Vector3 MousePos);

}
