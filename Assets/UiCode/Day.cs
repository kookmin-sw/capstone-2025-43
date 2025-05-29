using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day
{
    /// <summary>
    /// day = 2 afternoon = 1 night = 0
    /// take : num * 10 %
    /// </summary>
    int time;
    int take    ;
    public void setDay(int t,int p)
    {
        time = t;
        take = p;
    }

    public void passDay()
    {
        while(time >= 0)
        {
            switch (time)
            {
                case 1:
                    afternoon();
                    break;
                case 0:
                    night();
                    break;
            }
            time--;
        }

        time = 3;
    }

    void night()
    {
        //Heal();
        Managers.Data.localInfos[Managers.Data.handOverData.openLocal].side = "Ally";
        if (Random.Range(0, 10) >= take)
            TakenAlly();
    }

    void afternoon()
    {
        //Heal();
        if (Random.Range(0, 10) >= take)
            TakenAlly();
    }

    void TakenAlly()
    {
        List<Edge> attack = Managers.Game.map.GetLines();
        int t = Random.Range(0, attack.Count);
        Edge cur = attack[t];
        LocalInfo a = Managers.Data.localInfos[cur.v0];
        LocalInfo b = Managers.Data.localInfos[cur.v1];
        if (a.side == "Ally")
        {
            a.side = "Enemy";
            a.SetStages();
        }
        else
        {
            b.side = "Enemy";
            b.SetStages();
        }
    }

    void Heal()
    {
        //heal : poolmanager -> own hero
        foreach (var hero in Managers.Pool.heroPool.Values)
        {
            CharacterStat stat = hero.GetComponent<CharacterStat>();
            stat.hp = stat.hp + (stat.hp / 10);
            if (stat.hp > stat.hp_max)
            {
                stat.hp = stat.hp_max;
            }
            stat.mp = stat.mp_max;
        }
    }
}
