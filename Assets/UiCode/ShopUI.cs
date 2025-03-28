using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public HeroInventory heroInventory; // 플레이어가 보유한 영웅 목록
    public UnitData[] availableHeroes; // 상점에서 판매하는 영웅 목록
    public Transform contentPanel;
    public GameObject heroPrefab;

    void Start()
    {
        PopulateShop();
    }

    void PopulateShop()
    {
        foreach (UnitData hero in availableHeroes)
        {
            // 이미 보유한 영웅이면 스킵
            if (heroInventory.HasHero(hero)) continue;

            GameObject newHero = Instantiate(heroPrefab, contentPanel);
            ShopHeroUI heroUI = newHero.GetComponent<ShopHeroUI>();
            heroUI.Setup(hero, this);
        }
    }

    public void BuyHero(UnitData hero)
    {
        if (!heroInventory.HasHero(hero))
        {
            heroInventory.AddHero(hero);
            Debug.Log(hero.unitName + "을(를) 구매했습니다!");
            RefreshShop(); // 구매 후 상점 UI 업데이트
        }
    }

    void RefreshShop()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        PopulateShop();
    }
}
