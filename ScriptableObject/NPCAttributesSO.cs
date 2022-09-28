using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Greenvillex/NPCAttributes")]
public class NPCAttributesSO : ScriptableObject {
    public string nameUser;
    public float speedUser;
    public float jumpPower;
    //public List<ItemSO> listItems;

    public string GetNameUser()
    {
        return nameUser;
    }

    public float GetSpeedUser()
    {
        return speedUser;
    }

    public float GetJumpPower()
    {
        return jumpPower;
    }
}