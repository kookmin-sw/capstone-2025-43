using UnityEngine;
using UnityEngine.UIElements;

public class ListIdx : MonoBehaviour
{
    public UnitData data;
    Slider hpSlider;
    Slider mpSlider;
    public Sprite unitImg;
    private void Start()
    {
        unitImg = transform.GetChild(0).gameObject.GetComponent<Sprite>();
        hpSlider = transform.GetChild(1).gameObject.GetComponent<Slider>();
        mpSlider = transform.GetChild(2).gameObject.GetComponent<Slider>();

        unitImg = data.unitImage;
        SetHpBar();
        SetMpBar();
    }
    public void SetHpBar()
    {
        hpSlider.value = (float)data.curHp / data.maxHp;
    }
    public void SetMpBar()
    {
        mpSlider.value = (float)data.curMp / data.maxMp;
    }
}
