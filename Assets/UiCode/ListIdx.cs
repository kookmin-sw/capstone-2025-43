using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListIdx : MonoBehaviour
{
    public UnitData unitData;
    public Slider hpSlider;
    public Slider mpSlider;
    public Image unitImg;
    public Image more;
    public TMP_Text unitName;

    public void Init(UnitData data)
    {
        unitImg = transform.GetChild(0).gameObject.GetComponent<Image>();
        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        unitName = transform.GetChild(4).GetComponent<TMP_Text>();
        more = transform.GetChild(3).gameObject.GetComponent<Image>();
        var clickEvent = more.GetComponent<MoreClickEvent>();
        clickEvent.listIdx = this;
        unitData = data;
        hpSlider = sliders[0];
        mpSlider = sliders[1];

        unitImg.sprite = data.unitImage;
        unitName.text = this.name = data.unitName;
        hpSlider.value = (float)data.curHp / data.maxHp;
        mpSlider.value = (float)data.curMp / data.maxMp;
    }

    public void SetParent(Transform parent)
    {
        this.gameObject.transform.parent = parent;
    }
}
