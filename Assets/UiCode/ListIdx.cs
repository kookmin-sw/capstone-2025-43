using UnityEngine;
using UnityEngine.UIElements;

public class ListIdx : MonoBehaviour
{
    public UnitData data;
    public Slider hpSlider;
    public Slider mpSlider;
    public Sprite unitImg;
    private void Start()
    {
        unitImg = transform.GetChild(0).GetComponent<Sprite>();
        hpSlider = transform.GetChild(1).GetComponent<Slider>();
        mpSlider = transform.GetChild(2).GetComponent<Slider>();

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
