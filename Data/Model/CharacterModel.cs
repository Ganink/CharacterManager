using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    public string name;
    public string idToken;
    public int currentLevel;
    public int xp;
    public int gold;
    public int gems;
    public int strong;
    public int speed;
    public int defense;
    public float currentPositionX;
    public float currentPositionY;

    public List<InventoryItem> inventoryItems;
    public DateTime latestLoggin;
}
