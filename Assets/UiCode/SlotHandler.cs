using TMPro;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{

    public Transform dropContent;

    public void Buy()
    {
        Debug.Log($"shop : {Managers.Game.canChange("Shop")}");
        if (!Managers.Game.canChange("Shop")) return;

        foreach (Transform child in dropContent)
        {
            ListIdx idx = child.GetComponent<ListIdx>();
            CharacterStat stat = idx.unitData.GetComponent<CharacterStat>();
            stat.own = true;
            Managers.Data.handOverData.ownHero.Add(stat.DisplayName);
            Managers.Game.gold -= stat.price;
        }
        Managers.Ui.shopUi.GetComponent<DropTextHandler>().UpdateCur(0);
        Managers.Ui.shopUi.GetComponent<DropTextHandler>().UpdateMax(Managers.Game.gold);
        Managers.Ui.defaultUi.GetComponent<DropTextHandler>().UpdateMax(Managers.Game.gold);
        ClearCart();
    }
    public int cartPrice()
    {
        int price = 0;
        foreach (Transform child in dropContent)
        {
            ListIdx idx  = child.GetComponent<ListIdx>();
            CharacterStat stat = idx.unitData.GetComponent<CharacterStat>();
            price += stat.price;
        }
        return price;
    }

    public int selectHero()
    {
        int cnt = 0;
        foreach(Transform child in dropContent)
        {
            if(child.childCount > 0)
            {
                cnt++;
            }
        }
        return cnt;
    }

    public void StartBattle()
    {
        Debug.Log($"Local : {Managers.Game.canChange("Local")}");
        if (!Managers.Game.canChange("Local")) return;
        for (int idx = 0; idx < dropContent.childCount; idx++)
        {
            Transform child = dropContent.GetChild(idx);
            if(child.childCount > 0)
            {
                TMP_Text name = child.GetChild(0).GetComponent<ListIdx>().unitName;
                if (name != null)
                {
                    Managers.Data.handOverData.unitPositions[idx] = name.text;
                }
            }
            else
                Managers.Data.handOverData.unitPositions[idx] = null;
        }
        Managers.Game.StartBattle();
    }

    public void ClearPositionGrid()
    {
        for (int idx = 0; idx < dropContent.childCount; idx++)
        {
            Transform child = dropContent.GetChild(idx);
            if (child.childCount > 0)
            {
                child.GetChild(0).GetComponent<Drag>().returnToFrom();
            }
        }
    }

    public void ClearCart()
    {
        foreach (Transform child in dropContent)
            Destroy(child.gameObject);
    }

}
