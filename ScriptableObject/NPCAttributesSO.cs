using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Greenvillex/NPCAttributes")]
public class NPCAttributesSO : ScriptableObject {
    public string nameUser;
    public float speedUser;
    public float jumpPower;
    public float lifePoints;
    public float manaPoints;
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

    public float GetLifePoints()
    {
        return lifePoints;
    }

    public float GetManaPoints()
    {
        return manaPoints;
    }
}