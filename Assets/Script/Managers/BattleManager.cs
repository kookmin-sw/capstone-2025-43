using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using MyProject.Utils;
using System.Threading;
using UnityEngine.TextCore.Text;
using UnityEngine.AddressableAssets;

using UnityEngine.ResourceManagement.AsyncOperations;



#if UNITY_EDITOR
using Unity.VisualScripting;
#endif
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [HideInInspector] public GameObject player;
    [HideInInspector] public List<GameObject> flags = new();

    [HideInInspector] public List<Character> battleCharacter = new();
    [HideInInspector]public  List<Character> playerHeroes = new();
    private List<List<Character>> waveMonster = new();

    private int monsterCount = 0;
    private int heroCount = 0;
    private int currentWaveCount = 0;

#if UNITY_EDITOR
    //TODO:: Change To Map Handover Data
    public string[] testunitPositions;
    public List<BattleWavePreset> TestWaveList;
#endif
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // TESTING PURPOSE
        {
            //TODO:: Change To Map Handover Data
            FieldManager.Instance.InitializeRandomField(E_FieldType.Desert);
            InitializeFlag(TestWaveList);
            InitializePlayer();
            player.transform.position = flags[0].transform.position;
            InitializeMonsterWave(TestWaveList);
            if (Managers.instance)
                InitializePlayerHeroes(Managers.instance.dataManager.handOverData.unitPositions);
            else
                InitializePlayerHeroes(testunitPositions);
            WaveStart(currentWaveCount);
        }
    }

    private int ActivateCharacters(List<Character> characters, bool addInBattleCharacter = true, bool tacticSystemActive = true)
    {
        int count = 0;
        foreach (Character character in characters)
        {
            if(addInBattleCharacter)
                battleCharacter.Add(character);

            character.gameObject.SetActive(true);
            character.tacticSystem.isActive = tacticSystemActive;
            count++;
        }
        return count;
    }
    private void DeactivateCharacters(List<Character> characters, bool destroy = false)
    {
        foreach (Character character in characters)
        {
            if (destroy)
            {
                Destroy(character.gameObject);
            }
            else
            {
                if (character.Hp <= 0)
                    character.gameObject.SetActive(false);
            }
        }
    }
    #region Battle Flow
    public void WaveStart(int waveCount)
    {
        //in FirstWave
        if (waveCount == 0)
        {
            battleCharacter.Clear();
            heroCount = ActivateCharacters(playerHeroes);
            monsterCount = ActivateCharacters(waveMonster[waveCount]);
            player.GetComponent<PlayerController>().SetControlDirection(flags[currentWaveCount].transform.forward);
            player.GetComponent<PlayerController>().RotateTowardsDirection(flags[currentWaveCount].transform.forward);
            foreach (Character character in battleCharacter)
            {
                character.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
        else
        {
            foreach (Character monster in waveMonster[waveCount])
            {
                battleCharacter.Add(monster);
            }
            foreach (Character character in battleCharacter)
            {
                character.tacticSystem.isActive = true;
            }
        }

    }
    public void OnCharacterDied(Character character)
    {
        if (character.IsMonster)
            monsterCount--;
        else
            heroCount--;
        Debug.Log(heroCount);
        if (monsterCount <= 0)
        {
            Debug.Log("Wave End: All Monsters Defeated");
            WaveEnd();
            return;
        }

        if (heroCount <= 0)
        {
            Debug.Log("Wave End: All Heroes Defeated");
            //Show Defeat UI
            if (TacticUIManager.Instance != null)
            {
                TacticUIManager.Instance.OpenResultUI(false);
            }

        }
    }

  
    public void WaveEnd()
    {
        foreach (Character character in battleCharacter)
            character.tacticSystem.isActive = false;

        if (monsterCount == 0)
        {
            battleCharacter.RemoveAll(c => c != null && c.Hp <= 0);
        }
        currentWaveCount++;
        if (currentWaveCount >= waveMonster.Count)
        {
            //Show Victory UI
            Debug.Log("Victory!!!");
            TacticUIManager.Instance.OpenResultUI(true);
        }
        else
        {
            MoveToNextWave();
        }
    }

    #endregion

    #region Character Movement

    private async void MoveToNextWave()
    {
        // First Move
        Task heroesMove1 = MoveHeroes(battleCharacter, flags[currentWaveCount - 1].transform, 3f);
        Task playerMove1 = MovePlayer(flags[currentWaveCount - 1].transform, 3f);
        await Task.WhenAll(heroesMove1, playerMove1);
        //FirstMovend
        monsterCount = ActivateCharacters(waveMonster[currentWaveCount], false, false);
        await Task.Yield();

        // Second Move
        Task heroesMove2 = MoveHeroes(battleCharacter, flags[currentWaveCount].transform, 3f);
        Task playerMove2 = MovePlayer(flags[currentWaveCount].transform, 3f);
        await Task.WhenAll(heroesMove2, playerMove2);
        //SecondMoveEnd
        DeactivateCharacters(waveMonster[currentWaveCount - 1]);
        DeactivateCharacters(playerHeroes);
        waveMonster[currentWaveCount - 1].Clear();
        await Task.Yield();

        //PlayerRotation , Character Rotation
        Vector3 lookDir = flags[currentWaveCount].transform.forward;
        player.GetComponent<PlayerController>().SetControlDirection(lookDir);
        Task playerRotation = player.GetComponent<PlayerController>().RotateToDirection(lookDir, 3f);
        List<Task> heroRotations = new List<Task>();
        foreach (var hero in playerHeroes)
        {
            heroRotations.Add(hero.RotateToDirection(lookDir, 3f));
        }
        await Task.WhenAll(heroRotations.Append(playerRotation));
        WaveStart(currentWaveCount);
    }
    private async Task MovePlayer(Transform target, float delay, float moveSpeed = 8f)
    {
        if (player == null) return;

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.isPassive = true;

        await Task.Delay((int)(delay * 1000));

        Vector3 start = player.transform.position;
        Vector3 end = GridPositionUtil.GetGridPosition(E_GridPosition.Central, 3f, target);

        float distance = Vector3.Distance(start, end);
        float duration = distance / moveSpeed;
        float timeElapsed = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(playerController.GetControlDirection());
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);

            player.transform.position = Vector3.Lerp(start, end, t);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, t);

            await Task.Yield();
        }
        player.transform.position = end;
        player.transform.rotation = targetRotation;

        playerController.isPassive = false;
    }
    private async Task MoveHeroes(List<Character> characters, Transform target, float delay, float moveSpeed = 10f)
    {
        Task[] moveTasks = characters.Select(character =>
        {
            NavMeshAgent agent = character.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 0.1f;
            Vector3 destination = GridPositionUtil.GetGridPosition(character.gridposition, 3f, target);
            return MoveToPosition(agent, destination, delay, moveSpeed, character.MoveSpeed_origin);
        }).ToArray();

        await Task.WhenAll(moveTasks);
    }
    private Task MoveToPosition(NavMeshAgent agent, Vector3 destination, float delay, float moveSpeed, float restoreSpeed)
    {
        var tcs = new TaskCompletionSource<bool>();
        StartCoroutine(DelayedMove(agent, destination, delay, moveSpeed, restoreSpeed, tcs));
        return tcs.Task;
    }
    private IEnumerator DelayedMove(NavMeshAgent agent, Vector3 destination, float delay, float moveSpeed, float restoreSpeed, TaskCompletionSource<bool> tcs)
    {
        if (!agent.enabled)
        {
            Debug.LogWarning($"NavMeshAgent {agent.gameObject.name} is disabled!");
            tcs.SetResult(false);
            yield break;
        }

        yield return new WaitForSeconds(delay);

        Character character = agent.GetComponent<Character>();
        if (character != null)
            character.anim.SetMoveState(true, moveSpeed);

        float originalSpeed = agent.speed;
        agent.speed = moveSpeed;
        agent.isStopped = false;
        agent.SetDestination(destination);

        yield return new WaitUntil(() => !agent.pathPending);

        while (agent.remainingDistance > agent.stoppingDistance)
            yield return null;

        agent.isStopped = true;
        agent.speed = restoreSpeed;

        if (character != null)
            character.anim.SetMoveState(false, 0f);

        tcs.SetResult(true);
    }
    #endregion

    #region Initialization

    public void InitializePlayer()
    {
        if (player != null) return;

        int playerLayer = LayerMask.NameToLayer("Player");
        if (playerLayer == -1)
        {
            Debug.LogError("Layer 'Player' not found.");
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
        Debug.LogError("No object with 'Player' layer found in scene.");
    }

    public void InitializeFlag(List<BattleWavePreset> waves)
    {
        int requiredCount = waves.Count;
        while (flags.Count < requiredCount)
        {
            GameObject flag = new GameObject($"WaveFlag_{flags.Count}");
            flags.Add(flag);
        }
    }

    public void InitializeFlag(GameObject battleFieldPrefab)
    {
        flags.Clear();

        var planes = battleFieldPrefab.GetComponentsInChildren<Transform>()
                                      .Where(t => t.name == "Plane");
        foreach (var plane in planes)
        {
            var flag = plane.GetComponentsInChildren<Transform>()
                            .FirstOrDefault(t => t.name == "Flag");
            if (flag != null)
            {
                flags.Add(flag.gameObject);
            }
        }
        Debug.Log($"[FlagManager] {flags.Count} Flag Initialized");
    }

    public void InitializeMonsterWave(List<BattleWavePreset> waves)
    {
        waveMonster.Clear();
        for (int i = 0; i < waves.Count; i++)
        {
            waveMonster.Add(new List<Character>());
            waves[i].CreateMonster(flags[i], i);
        }
    }

    public void InitializePlayerHeroes(string[] unitPositions)
    {
        playerHeroes.Clear();

        if (flags.Count == 0)
        {
            Debug.LogError("No flags initialized.");
            return;
        }

        for (int i = 0; i < unitPositions.Length; i++)
        {
            string heroID = unitPositions[i];
            if (string.IsNullOrEmpty(heroID))
                continue;

            AsyncOperationHandle handle = Addressables.LoadAssetAsync<GameObject>(heroID);
            handle.WaitForCompletion();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"[Hero Init] Failed to load hero prefab: {heroID}");
                continue;
            }

            GameObject prefab = (GameObject)handle.Result;
            GameObject obj = Instantiate(prefab);

            if (!obj.TryGetComponent(out Character character))
            {
                Debug.LogError($"[Hero Init] Prefab {heroID} has no Character component.");
                Destroy(obj);
                continue;
            }

            E_GridPosition gridPosition = (E_GridPosition)i+1;

            character.gridposition = gridPosition;
            character.transform.position = GridPositionUtil.GetGridPosition(gridPosition, 3f, flags[0].transform);
            character.gameObject.SetActive(false);

            playerHeroes.Add(character);
        }
    }

    public void InitializePlayerHeroes(List<HeroSpawnData> spawnDataList)
    {
        playerHeroes.Clear();

        if (flags.Count == 0)
        {
            Debug.LogError("No flags initialized.");
            return;
        }

        foreach (HeroSpawnData data in spawnDataList)
        {
            string address = data.heroID;

            AsyncOperationHandle handle = Addressables.LoadAssetAsync<GameObject>(address);
            handle.WaitForCompletion();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"[Hero Init] Failed to load hero prefab: {address}");
                continue;
            }

            GameObject prefab = (GameObject)handle.Result;
            GameObject obj = Instantiate(prefab);

            if (!obj.TryGetComponent(out Character character))
            {
                Debug.LogError($"[Hero Init] Prefab {address} has no Character component.");
                Destroy(obj);
                continue;
            }

            character.gridposition = data.gridPosition;
            character.transform.position = GridPositionUtil.GetGridPosition(data.gridPosition, 3f, flags[0].transform);
            character.gameObject.SetActive(false);

            playerHeroes.Add(character);
        }
    }

    public void AddMonsterinWave(Character character, int waveNumber)
    {
        if (waveNumber < 0 || waveNumber >= waveMonster.Count)
        {
            Debug.LogError($"Invalid wave number: {waveNumber}");
            return;
        }

        waveMonster[waveNumber].Add(character);
    }

    #endregion
}
