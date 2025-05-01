using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class ResultUI : MonoBehaviour
{
    public GameObject panel;
    public Button sceneChangeButton;
    [SerializeField] private TMP_Text ResultText;

    [SerializeField] private GameObject[] characterSlots; // TacticSlot Count is Always 4
    [SerializeField] private GameObject[] rewardSlot; // TacticSlot Count is Always 7
    private bool result = false;
    private void Start()
    {
        AudioManager.Instance.StopBGM(); // Test Stop BGM
    }
    public void OpenUI(bool isWinning)
    {
        panel.SetActive(true);
        InitializeSceneChangeButton();
        InitializeResultText(isWinning);
        InitializeCharacterSlot();
        InitializeRewardSlot();
    }
    private void InitializeSceneChangeButton()
    {
        sceneChangeButton.onClick.RemoveAllListeners();
        sceneChangeButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            if (Managers.Instance != null && Managers.Game != null)
            {
                Managers.Game.EndBattle(result);
            }
        });
    }
    private void InitializeResultText(bool isWinning)
    {
        result = isWinning;
        ResultText.text = isWinning ? "Victory!!" : "Defeat!!";
        ResultText.color = isWinning ? Color.yellow : Color.red;

    }
    private void InitializeCharacterSlot()
    {
        foreach(GameObject characterSlot in characterSlots)
        {
            characterSlot.SetActive(false);
        }
        if(BattleManager.Instance)
        {
            int count = 0;
            List<Character> heroes = BattleManager.Instance.playerHeroes;
            if(heroes.Count > 4)
            {
                Debug.LogError("BattleFieldHero Cannot be over 4");
                return;
            }
            foreach (Character hero in heroes)
            {
                characterSlots[count].SetActive(true);
                Image CharacterIcon = characterSlots[count].transform.Find("CharacterIcon").GetComponent<Image>();
                CharacterIcon.sprite = hero.LoadIcon();
                TMP_Text displayNameUI = characterSlots[count].transform.Find("CharacterDisplayName").GetComponent<TMP_Text>();
                displayNameUI.SetText(hero.DisplayName);
                TMP_Text HpRemainUI = characterSlots[count].transform.Find("CharacterHPRemain").GetComponent<TMP_Text>();
                HpRemainUI.SetText(string.Format("Hp : {0} / {1}", hero.Hp, hero.HpMax));
                ++count;
            }
        }
    }
    private void InitializeRewardSlot()
    {
        //TODO :: After Reward Information Added, Display UI Icon

    }
}
