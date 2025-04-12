using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using MyProject.Utils;
using System.Collections;
using UnityEngine.AI;
using System.Threading.Tasks;

[RequireComponent(typeof(CharacterStat))]
public class Character : MonoBehaviour
{
    public CharacterStat stat;
    public E_GridPosition gridposition = E_GridPosition.Empty;
    public CharacterAnimation anim;
    public NavMeshAgent agent;

    [HideInInspector] public TacticSystem tacticSystem;
    private void Awake()
    {
        stat = GetComponent<CharacterStat>();
        tacticSystem = GetComponent<TacticSystem>();
        anim = GetComponent<CharacterAnimation>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        CharacterManager.Instance.RegisterCharacter(this, stat.isMonster);
        //agent.acceleration = float.MaxValue; 
        //agent.autoBraking = false;             

    }
    private void OnDestroy()
    {
        CharacterManager.Instance.UnregisterCharacter(this);
    }
    public async Task RotateToDirection(Vector3 direction, float rotateSpeed = 5f)
    {
        direction.y = 0;
        if (direction.normalized == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime * 100f);
            await Task.Yield();
        }

        transform.rotation = targetRotation;
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
            animator.SetTrigger("Dying");
        }
        if (TryGetComponent(out TacticSystem tacticSystem))
        {
            tacticSystem.isActive = false;
        }
        BattleManager.Instance.OnCharacterDied(this);
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
    public float MoveSpeed_origin
    {
        get => stat.moveSpeed_origin;
        set => stat.moveSpeed_origin = value;
    }

    public float MoveSpeed
    {
        get => stat.moveSpeed;
        set => stat.moveSpeed = value;
    }

    public float RotationSpeed_origin
    {
        get => stat.rotationSpeed_origin;
        set => stat.rotationSpeed_origin = value;
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
