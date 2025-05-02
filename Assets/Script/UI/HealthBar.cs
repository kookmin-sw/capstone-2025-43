using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject HeathPanel;
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private Character character;
    public float lerpspeed_easeHealth = 0.02f;
    void Start()
    {
        character = GetComponentInParent<Character>();
    }

    void Update()
    {
        healthSlider.value = character.Hp / character.HpMax;
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, character.Hp / character.HpMax, lerpspeed_easeHealth);
        }
        if (character.Hp <= 0)
            HeathPanel.SetActive(false);
    }
}
