using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class Status : MonoBehaviour
{
    public CharacterStat unitData;
    public Slider hpSlider;
    public Slider mpSlider;
    public Image unitImg;
    public TMP_Text damage;
    public TMP_Text unitName;
    public TMP_Text hp;
    public TMP_Text mp;
    public TMP_Text attackRange;
    public TMP_Text moveSpeed;
    public TMP_Text rotationSpeed;
    public TMP_Text targets;
    public TMP_Text conditions;
    public TMP_Text actions;


    public void Init(CharacterStat data)
    {
        unitData = data;

        unitName.text = this.name = data.DisplayName;
        unitImg.sprite = Managers.Resource.Load<Sprite>($"Character/ScreenShot/{unitName.text}");
        damage.text = $"Damage : {data.damage}";
        attackRange.text = $"AttackRange : {data.attackRange}";
        moveSpeed.text = $"MoveSpeed : {data.moveSpeed}";
        rotationSpeed.text = $"RotationSpeed : {data.rotationSpeed}";
        hp.text = $"{data.hp} / {data.hp_max}";
        mp.text = $"{data.mp} / {data.mp_max}";
        hpSlider.value = (float)data.hp / data.hp_max;
        mpSlider.value = (float)data.mp / data.mp_max;
        targets.text = $"Targets:\n{string.Join("\n", data.Targets)}";
        conditions.text = $"\nConditions:\n{string.Join("\n", data.Conditions)}";
        // Actions 출력
        List<string> actionDescriptions = new List<string>();
        foreach (var action in data.Actions)
        {
            if (action == null) continue; // 혹시 null 체크

            string desc = $"- {action.displayName}\n   Cooldown: {action.actionCooldown}\n   Effect Value: {data.damage + action.amount}\n   SingleTarget: {action.IsSingleTarget}";
            actionDescriptions.Add(desc);
        }

        actions.text = $"\nActions:\n{string.Join("\n", actionDescriptions)}";
    }
}