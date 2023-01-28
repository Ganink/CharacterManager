using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Greenvillex/NPCAttributes")]
public class NPCAttributesSO : ScriptableObject {
    [SerializeField] private string name;
    [SerializeField] private string idToken;
    [SerializeField] private int currentLevel;
    [SerializeField] private int xp;
    [SerializeField] private int gold;
    [SerializeField] private int gems;
    [SerializeField] private int strong;
    [SerializeField] private int speed;
    [SerializeField] private int defense;
    [SerializeField] private int maxLifesPoints;
    [SerializeField] private int maxManaPoints;

    public string Name { get => name; set => name = value; }
    public string IdToken { get => idToken; set => idToken = value; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public int Xp { get => xp; set => xp = value; }
    public int Gold { get => gold; set => gold = value; }
    public int Gems { get => gems; set => gems = value; }
    public int Strong { get => strong; set => strong = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Defense { get => defense; set => defense = value; }
    public int MaxLifesPoints { get => maxLifesPoints; set => maxLifesPoints = value; }
    public int MaxManaPoints { get => maxManaPoints; set => maxManaPoints = value; }
    //public List<ItemSO> listItems;
}