using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


[RequireComponent(typeof(CharacterStat))]
public class Character : MonoBehaviour
{
    public CharacterStat stat;
    [HideInInspector] public TacticSystem tacticSystem;
    private void Start()
    {
        stat = GetComponent<CharacterStat>();
        tacticSystem = GetComponent<TacticSystem>();
        CharacterManager.Instance.RegisterCharacter(this, stat.isMonster);
    }

    private void OnDestroy()
    {
        CharacterManager.Instance.UnregisterCharacter(this);
    }

    public void AddTacticComponent(Tactic tactic)
    {
        if (!stat.Targets.Contains(tactic.targetType))
        {
            stat.Targets.Add(tactic.targetType);
            Debug.Log("Added targetType: " + tactic.targetType);
        }

        if (!stat.Conditions.Contains(tactic.conditionType))
        {
            stat.Conditions.Add(tactic.conditionType);
            Debug.Log("Added conditionType: " + tactic.conditionType);
        }

        if (!stat.Actions.Contains(tactic.actionType))
        {
            stat.Actions.Add(tactic.actionType);
            Debug.Log("Added actionType: " + tactic.actionType);
        }
    }
    public bool IsEnemy(Character other)
    {
        return this.stat.isMonster != other.stat.isMonster && this.stat.team_id != other.stat.team_id;
    }
    public bool IsAlly(Character other)
    {
        return this.stat.team_id == other.stat.team_id;
    }

    public void AddHP(float amount)
    {
        stat.hp += amount;

        if (stat.hp > stat.hp_max)
        {
            stat.hp = stat.hp_max;
        }
       if(stat.hp <= 0)
        {
            stat.hp = 0;
            Die();
        }
    }

    public void ApplyDamage(float amount)
    {
        stat.hp -= amount;

        if (stat.hp > stat.hp_max)
        {
            stat.hp = stat.hp_max;
        }
        if (stat.hp <= 0)
        {
            stat.hp = 0;
            Die();
        }
    }

    public void Die()
    {
        Hp = 0;
        if (TryGetComponent(out Animator animator))
        {
            //animator.SetTrigger("Die");
            animator.SetBool("Dying", true);
        }
        if (TryGetComponent(out TacticSystem tacticSystem))
        {
            tacticSystem.enabled = false;
        }

        //Destroy(gameObject, 5f);
    }


    #region ValueWrappers
    public string DisplayName
    {
        get => stat.DisplayName;
        set => stat.DisplayName = value;
    }

    public bool IsMonster
    {
        get => stat.isMonster;
        set => stat.isMonster = value;
    }

    public int tacticCapacity
    {
        get => stat.tacticCapacity;
        set => stat.tacticCapacity = value;
    }


    public int TeamId
    {
        get => stat.team_id;
        set => stat.team_id = value;
    }

    public float GlobalCooldown
    {
        get => stat.GlobalCooldown;
        set => stat.GlobalCooldown = value;
    }

    public float HpMax
    {
        get => stat.hp_max;
        set => stat.hp_max = value;
    }

    public float Hp
    {
        get => stat.hp;
        set => stat.hp = value;
    }

    public float MpMax
    {
        get => stat.mp_max;
        set => stat.mp_max = value;
    }

    public float Mp
    {
        get => stat.mp;
        set => stat.mp = value;
    }

    public float DamageMax
    {
        get => stat.damage_max;
        set => stat.damage_max = value;
    }

    public float Damage
    {
        get => stat.damage;
        set => stat.damage = value;
    }

    public float AttackRangeMax
    {
        get => stat.attackRange_Max;
        set => stat.attackRange_Max = value;
    }

    public float AttackRange
    {
        get => stat.attackRange;
        set => stat.attackRange = value;
    }

    public float MoveSpeed
    {
        get => stat.moveSpeed;
        set => stat.moveSpeed = value;
    }

    public float RotationSpeed
    {
        get => stat.rotationSpeed;
        set => stat.rotationSpeed = value;
    }

    public List<TargetType> Targets => stat.Targets;
    public List<ConditionType> Conditions => stat.Conditions;
    public List<ActionType> Actions => stat.Actions;


    #endregion
}
