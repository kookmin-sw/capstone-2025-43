using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Utils;
using Unity.VisualScripting;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Rendering;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public GameObject player;

    public List<GameObject> flags = new List<GameObject>();

    [HideInInspector] public List<Character> battleCharacter = new List<Character>(); //current Battle Characters include Heroes and Monsters

    private List<Character> playerHeroes = new List<Character>(); //PlayerHeroes Information
    private List<List<Character>> waveMonster = new List<List<Character>>(); //Each Wave's Monster Information

    int monsterCount = 0;
    int heroCount = 0;
    int currentWaveCount = 0;
    bool waveMoving = false;

#if UNITY_EDITOR
    public List<BattleWavePreset> TestWaveList;
    public List<GameObject> TestPlayerList;
#endif

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InitializePlayer();
        //TODO :: TEST!!TEST!!TEST!!TEST!!TEST!! Delete this test code 
        {
            InitializeFlag(TestWaveList);
            InitializeMonsterWave(TestWaveList);
            InitializePlayerHeroes(TestPlayerList);
            WaveStart(currentWaveCount);
        }
    }
    private void Update()
    {
        if (waveMoving)
        {
            bool allHeroesArrived = true;

            foreach (Character hero in battleCharacter)
            {
                NavMeshAgent agent = hero.GetComponent<NavMeshAgent>();
                if (agent != null && !agent.pathPending)
                {
                    if (agent.remainingDistance > agent.stoppingDistance)
                    {
                        allHeroesArrived = false;
                    }
                }
            }

            if (allHeroesArrived)
            {
                waveMoving = false;
                WaveStart(currentWaveCount);
                //ChangePlayerMoveRestrictBoundary();
            }
        }

    }
    public void InitializePlayer()
    {
        if (player == null)
        {
            int playerLayer = LayerMask.NameToLayer("Player");

            if (playerLayer == -1)
            {
                Debug.LogError("InitializePlayer: Layer 'Player' does not exist.");
                return;
            }

            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach (GameObject obj in allObjects)
            {
                if (obj.layer == playerLayer)
                {
                    player = obj;
                    return;
                }
            }
            Debug.LogError("InitializePlayer: No GameObject with layer 'Player' found in the scene.");
        }
    }


    //Check Wave is Finished(All Heroes Dead or All Monsters Dead)
    public void WaveEnd()
    {
        foreach (Character character in battleCharacter)
        {
            TacticSystem tacticSystem = character.GetComponent<TacticSystem>();
            tacticSystem.isActive = false;
        }
        //Remove PrevWaveMonster from battleCharacterList
        if (monsterCount == 0)
        {
            battleCharacter.RemoveAll(character =>
            {
                Character c = character.GetComponent<Character>();
                return c != null && c.Hp <= 0;});
        }

        ++currentWaveCount;
        if (currentWaveCount >= waveMonster.Count)
        {
            //TODO :: Popup ResultUi _ WinGame
        }
        else
        {
            //TODO :: heroes and player Move To NextFlag ~> If MoveFinished : WaveStart(currentWaveCount) , Change PlayerMoveRestrictBoundary
            StartCoroutine(MoveHeroesAndProceed());
        }
    }
    int finishedMovements = 0;
    private IEnumerator MoveHeroesAndProceed()
    {
        finishedMovements = 0;
        int totalHeroes = battleCharacter.Count;

        foreach (Character hero in battleCharacter)
        {
            NavMeshAgent agent = hero.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 0.1f;
            Vector3 destination = GridPositionUtil.GetGridPosition(hero.gridposition, 3f, flags[currentWaveCount - 1].transform.position);

            StartCoroutine(DelayedMove(destination, agent, 3f));
        }
        //Wait All Character is Move is Finished
        while (finishedMovements < totalHeroes)
        {
            yield return null;
        }
        Debug.Log("Finish Movement");

        finishedMovements = 0;

        // TODO :: heroes and player Move To NextFlag ~> If MoveFinished : WaveStart(currentWaveCount) , Change PlayerMoveRestrictBoundary
        foreach (Character hero in battleCharacter)
        {
            NavMeshAgent agent = hero.GetComponent<NavMeshAgent>();
            Vector3 destination = GridPositionUtil.GetGridPosition(hero.gridposition, 3f, flags[currentWaveCount].transform.position);

            StartCoroutine(DelayedMove(destination, agent, 3f));
        }
        while (finishedMovements < totalHeroes)
        {
            yield return null;
        }
        Debug.Log("Finish Movement");
    }

    private IEnumerator DelayedMove(Vector3 destination, NavMeshAgent agent, float delay)
    {
        yield return new WaitForSeconds(delay);
        agent.SetDestination(destination);

        // Check Moving is Finish
        while (true)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                agent.ResetPath();
                break;
            }
            yield return null;
        }
        finishedMovements++;
    }

    public void WaveStart(int waveCount)
    {
        if (waveCount >= waveMonster.Count)
        {
            Debug.LogError("Invalid wave count");
            return;
        }
        if (waveCount == 0)
        {
            battleCharacter.Clear();
            heroCount = 0;
            foreach (Character hero in playerHeroes)
            {
                ++heroCount;
                hero.gameObject.SetActive(true);
                battleCharacter.Add(hero);
            }
        }
        monsterCount = 0;
        List<Character> currentWave = waveMonster[waveCount];
        foreach (Character monster in currentWave)
        {
            ++monsterCount;
            monster.gameObject.SetActive(true);
            battleCharacter.Add(monster);
        }
    }

    public void InitializeFlag(List<BattleWavePreset> waves)
    {
        int waveCount = waves.Count;
        int flagCount = flags.Count;

        for (int i = flagCount; i < waveCount; ++i)
        {
            GameObject flag = new GameObject($"WaveFlag_{i}");
            flags.Add(flag);
        }
    }
    public void InitializeMonsterWave(List<BattleWavePreset> waves)
    {
        int waveCount = waves.Count;
        waveMonster.Clear();
        for (int i = 0; i < waveCount; ++i)
        {
            waveMonster.Add(new List<Character>());
        }

        for (int i = 0; i < waveCount; ++i)
        {
            waves[i].CreateMonster(flags[i], i);
        }
    }
    public void InitializePlayerHeroes(List<GameObject> heroes)
    {
        playerHeroes.Clear();
        foreach (GameObject obj in heroes)
        {
            Character character = obj.GetComponent<Character>();
            if (character != null)
            {
                playerHeroes.Add(character);
            }
            else
            {
                Debug.LogError($"GameObject {obj.name} does not have a Character component.");
            }
        }

        if (flags.Count == 0)
        {
            Debug.LogError("No flags found!");
            return;
        }

        GameObject currentflag = flags[0];

        foreach (Character hero in playerHeroes)
        {
            hero.transform.position = GridPositionUtil.GetGridPosition(hero.gridposition, 3f, currentflag.transform.position);
            hero.gameObject.SetActive(false);
        }
    }
    public void AddMonsterinWave(Character character, int waveNumber)
    {
        if (waveNumber >= waveMonster.Count)
        {
            Debug.LogError($"Invalid waveNumber: {waveNumber}");
            return;
        }
        waveMonster[waveNumber].Add(character);
    }

    public void OnCharacterDied(Character character) //Character.Die() ~> Call this Function
    {
        if (character.IsMonster)
            --monsterCount;
        else
            --heroCount;
        if (monsterCount <= 0)
        {
            Debug.Log("Wave is End _ Monster All Dead");
            WaveEnd();
            return;
        }
        if (heroCount <= 0)
        {
            Debug.Log("Wave is End _ Heroes All Dead");
            //TODO :: Popup ResultUi _ Lose Game
        }
    }
}
