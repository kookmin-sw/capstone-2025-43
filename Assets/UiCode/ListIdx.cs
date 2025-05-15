using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListIdx : MonoBehaviour
{
    public CharacterStat unitData;
    public Slider hpSlider;
    public Slider mpSlider;
    public Image unitImg;
    public TMP_Text unitName;
    public TMP_Text hpText;
    public TMP_Text mpText;

    public void Init(CharacterStat data)
    {
        unitData = data;
        //unitImg = data.unitImage;
        unitName.text = this.name = data.DisplayName;
        hpSlider.value = (float)data.hp / data.hp_max;
        mpSlider.value = (float)data.mp / data.mp_max;

        hpText.text = $"{data.hp} / {data.hp_max}";
        mpText.text = $"{data.mp} / {data.mp_max}";

        unitImg.sprite = Managers.Resource.Load<Sprite>($"Character/ScreenShot/{unitName.text}");
    }

    public void SetParent(Transform parent)
    {
        this.gameObject.transform.parent = parent;
    }
}
