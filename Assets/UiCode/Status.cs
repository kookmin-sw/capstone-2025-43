using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    
    public void Init(CharacterStat data)
    {
        unitData = data;

        unitImg.sprite = Managers.Resource.Load<Sprite>($"Character/ScreenShot/{unitName.text}");
        damage.text = $"Damage : {data.damage}";
        attackRange.text = $"AttackRange : {data.attackRange}";
        moveSpeed.text = $"MoveSpeed : {data.moveSpeed}";
        rotationSpeed.text = $"RotationSpeed : {data.rotationSpeed}";
        unitName.text = this.name = data.DisplayName;
        hp.text = $"{data.hp} / {data.hp_max}";
        mp.text = $"{data.mp} / {data.mp_max}";
        hpSlider.value = (float)data.hp / data.hp_max;
        mpSlider.value = (float)data.mp / data.mp_max;
    }
}