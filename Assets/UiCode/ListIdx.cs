using UnityEngine;
using UnityEngine.UI;

public class ListIdx : MonoBehaviour
{
    public UnitData data;
    public Slider hpSlider;
    public Slider mpSlider;
    public Sprite unitImg;
    private void Start()
    {
        unitImg = transform.GetChild(0).gameObject.GetComponent<Sprite>();
        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        hpSlider = sliders[0];
        mpSlider = sliders[1];
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
