using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListIdx : MonoBehaviour
{
    public UnitData unitData;
    public Slider hpSlider;
    public Slider mpSlider;
    public Sprite unitImg;
    public TMP_Text unitName;
    public void Init(UnitData data)
    {
        unitImg = transform.GetChild(0).gameObject.GetComponent<Sprite>();
        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        unitName = transform.GetChild(4).GetComponent<TMP_Text>();
        unitData = data;
        hpSlider = sliders[0];
        mpSlider = sliders[1];

        unitImg = data.unitImage;
        unitName.text = this.name = data.unitName;
        hpSlider.value = (float)data.curHp / data.maxHp;
        mpSlider.value = (float)data.curMp / data.maxMp;
    }
}
