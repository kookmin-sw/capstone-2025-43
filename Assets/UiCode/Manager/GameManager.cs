using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager
{
    private int _time = 0; // 0: morning, 1: afternoon, 2: night
    public int gold;
    public Map map;
    public LocalData data;
    

    public int xBorderAlly = 0;
    public int yBorderAlly = 5;
    
    public float GameTime;
    public bool isPause = false;


    public void Init()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        map.Init();
        time = 0;
        gold = 1000;
    }

    public void StartBattle()
    {
        map.gameObject.SetActive(false);
        //todo start battle scene
        SceneManager.LoadScene("BattleScene");
    }

    // From BattleScene
    public void EndBattle(bool success)
    {
        SceneManager.LoadScene("MapScene");
        if (success)
        {
            time = 2;
            Managers.Data.handOverData.localInfos[Managers.Data.handOverData.openLocal].side = "Ally";
        }
        else
        {
            time = 1;
        }
        //Load Game
        TakenAlly();
    }
    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1f); // �� ������ ���
        Debug.Log("�Ƹ��� �� �ε� �Ϸ� ���Դϴ�!");
        Managers.Ui.Init();
        Managers.Pool.SetHeroList();
        ReloadGame();
        // �� ������Ʈ ���� ����
    }

    public void ReloadGame()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        map.CreateMap();
    }

    public void GamePause()
    {
        isPause = true;
        Time.timeScale = 0.0f;
    }
    public void GameResume()
    {
        isPause = false;
        Time.timeScale = 1.0f;
    }

    public bool inBorderAlly(Vector2 position)
    {
        return position.x < xBorderAlly && position.y < yBorderAlly;
    }


    public int time
    {
        get => _time;
        set
        {
            if (_time != value)
            {
                _time = value;
                OnTimeChanged();
            }
        }
    }

    private void OnTimeChanged()
    {
        Debug.Log($"[GameManager] Time changed to: {_time}");

        HealUnitsOnTimeChange();
    }

    private void HealUnitsOnTimeChange()
    {
        float healRatio = 0.2f; // 최대 HP의 20% 회복

        foreach (UnitData unit in Managers.Pool.ownHeroData)
        {
            int healAmount = Mathf.CeilToInt(unit.maxHp * healRatio);
            unit.curHp = Mathf.Min(unit.curHp + healAmount, unit.maxHp);

            Debug.Log($"{unit.unitName} 회복: +{healAmount} → 현재 HP: {unit.curHp}/{unit.maxHp}");
        }
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            //UpdateGoldUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        //UpdateGoldUI();
    }


    public void TakenAlly()
    {
        List<Line> attack = map.GetLines();
        int t = Random.Range(0, attack.Count);
        Line cur = attack[t];
        LocalInfo a = Managers.Data.handOverData.localInfos[cur.p0];
        LocalInfo b = Managers.Data.handOverData.localInfos[cur.p1];
        if(a.side == "Ally")
            a.side = "Enemy";
        else
            b.side = "Enemy";
    }

}
