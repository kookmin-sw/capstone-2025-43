using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Status : MonoBehaviour
{
    public UnitData unitData;
    public Slider hpSlider;
    public Slider mpSlider;
    public Image unitImg;
    public TMP_Text damage;
    public TMP_Text unitName;
    public TMP_Text hp;
    public TMP_Text mp;


    public void Init(UnitData data)
    {
        unitData = data;


        unitImg = transform.GetChild(4).gameObject.GetComponent<Image>();
        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        damage = transform.GetChild(5).GetComponent<TMP_Text>();
        unitName = transform.GetChild(6).GetComponent<TMP_Text>();
        hp = transform.GetChild(8).GetComponent<TMP_Text>();
        mp = transform.GetChild(9).GetComponent<TMP_Text>();
        
        hpSlider = sliders[0];
        mpSlider = sliders[1];

        unitImg.sprite = data.unitImage;
        damage.text = $"Damage : {data.damage}";
        unitName.text = data.unitName;
        hp.text = $"{data.curHp} / {data.maxHp}";
        mp.text = $"{data.curMp} / {data.maxMp}";
        hpSlider.value = (float)data.curHp / data.maxHp;
        mpSlider.value = (float)data.curMp / data.maxMp;
    }
}
