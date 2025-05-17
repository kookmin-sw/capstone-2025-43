using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day
{
    /// <summary>
    /// day = 2 afternoon = 1 night = 0
    /// </summary>
    int time;

    public void setTime(int t)
    {
        time = t;
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
        Heal();
        Managers.Data.handOverData.localInfos[Managers.Data.handOverData.openLocal].side = "Ally";
        TakenAlly();
    }

    void afternoon()
    {
        Heal();
        TakenAlly();
    }

    void day()
    {
        // day
    }
    void TakenAlly()
    {
        List<Edge> attack = Managers.Game.map.GetLines();
        int t = Random.Range(0, attack.Count);
        Edge cur = attack[t];
        LocalInfo a = Managers.Data.handOverData.localInfos[cur.v0];
        LocalInfo b = Managers.Data.handOverData.localInfos[cur.v1];
        if (a.side == "Ally")
            a.side = "Enemy";
        else
            b.side = "Enemy";
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
