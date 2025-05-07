using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListIdx : MonoBehaviour
{
    public CharacterStat unitData;
    public Slider hpSlider;
    public Slider mpSlider;
    public Sprite unitImg;
    public TMP_Text unitName;
    public void Init(CharacterStat data)
    {
        unitImg = transform.GetChild(0).gameObject.GetComponent<Sprite>();
        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        unitName = transform.GetChild(4).GetComponent<TMP_Text>();
        unitData = data;
        hpSlider = sliders[0];
        mpSlider = sliders[1];

        //unitImg = data.unitImage;
        unitName.text = this.name = data.DisplayName;
        hpSlider.value = (float)data.hp / data.hp_max;
        mpSlider.value = (float)data.mp / data.mp_max;
    }

    public void SetParent(Transform parent)
    {
        this.gameObject.transform.parent = parent;
    }
}
