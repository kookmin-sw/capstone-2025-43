using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public string DisplayName = "DisplayName";
    public bool isMonster = false;
    public bool own = false;
    public int tacticCapacity = 3;
    public int team_id = 1;
    [HideInInspector]public int price = 100;
    public float GlobalCooldown = 5;
    public float hp_max = 100;
    [HideInInspector]public float hp = 100;
    [HideInInspector] public float mp_max = 50;
    [HideInInspector] public float mp = 50;
    public float damage_max = 10;
    [HideInInspector] public float damage = 10;
    public float attackRange_Max = 3;
    [HideInInspector] public float attackRange = 3;
    public float moveSpeed_origin = 10;
    [HideInInspector] public float moveSpeed = 10;
    public float rotationSpeed_origin = 10;
    [HideInInspector] public float rotationSpeed= 10;
    public List<TargetType> Targets; // TargetList that Character Has
    public List<ConditionType> Conditions; // ConditionList that Character Has
    public List<ActionType> Actions; // ActionList that Character Has
}
