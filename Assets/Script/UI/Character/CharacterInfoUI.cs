using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CharacterInfoUI : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] private GameObject[] characterSlots; // CharacterInfoSlot Count is Always 4
    public Slider[] healthSlider;
    public Slider[] easeHealthSlider;
    public Slider[] TacticCooldownSlider;
    public TMP_Text[] TacticText;
    private float lerpspeed_easeHealth = 0.02f;
    private List<Character> heroes = new List<Character>();

    public void OpenUI()
    {
        panel.SetActive(true);
        InitializeCharacterSlots();
    }
    void InitializeCharacterSlots()
    {
        foreach (GameObject characterSlot in characterSlots)
        {
            characterSlot.SetActive(false);
        }
        if (BattleManager.Instance)
        {
            int count = 0;
            heroes = BattleManager.Instance.playerHeroes;
            if (heroes.Count > 4)
            {
                Debug.LogError("BattleFieldHero Cannot be over 4");
                return;
            }
            foreach (Character hero in heroes)
            {
                characterSlots[count].SetActive(true);
                //LoadIcon
                Image IconMask = characterSlots[count].transform.Find("IconMask").GetComponent<Image>();
                Image Icon = IconMask.transform.Find("Icon").GetComponent<Image>();
                Icon.sprite = hero.LoadIcon();
                TMP_Text displayNameUI = characterSlots[count].transform.Find("NameText").GetComponent<TMP_Text>();
                displayNameUI.SetText(hero.DisplayName);
                TacticText[count].SetText("");
                ++count;
            }
        }
    }
    void Start()
    {

    }

    void Update()
    {
        if (heroes.Count <= 0)
            return;

        int count = 0;
        foreach (Character hero in heroes)
        {
            Tactic tactic;
            if (tactic = hero.tacticSystem.currentTactic)
                TacticText[count].SetText(tactic.actionType.displayName + "!!");
            healthSlider[count].value = hero.Hp/hero.HpMax;
            if (healthSlider[count].value != easeHealthSlider[count].value)
            {
                easeHealthSlider[count].value = Mathf.Lerp(easeHealthSlider[count].value, hero.Hp / hero.HpMax, lerpspeed_easeHealth);
            }

            TacticCooldownSlider[count].value = hero.tacticSystem.cooldownTimer / hero.GlobalCooldown;
            count++;
        }
    }
}
