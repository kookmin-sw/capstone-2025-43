using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShopHeroUI : MonoBehaviour
{
    public Image heroImage;
    public TextMeshProUGUI heroNameText;
    public Button buyButton;
    private UnitData heroData;
    private ShopUI shopUI;

    public void Setup(UnitData hero, ShopUI manager)
    {
        heroData = hero;
        shopUI = manager;
        heroNameText.text = hero.heroName;
        heroImage.sprite = hero.heroImage;

        buyButton.onClick.AddListener(() => shopUI.BuyHero(heroData));
    }
}
