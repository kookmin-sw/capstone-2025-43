using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Action/LaserAction")]
public class LaserAction : ActionType
{
    public GameObject laserPrefab;
    public float damage = 100f;
    public float laserDuration = 3f;

    public override void Execute(Character user, List<Character> targets)
    {
        if (targets == null || targets.Count == 0) return;

        // 쿨타임 정지
        TacticSystem tacticSystem = user.tacticSystem;
        if (tacticSystem != null)
        {
            tacticSystem.stopCooldown = true;
        }

        // 회전 후 동작 코루틴 실행
        user.StartCoroutine(RotateAndAction(user, targets, user.agent, FireLaser)); // FireLaser 자체를 넘김

    }

    private IEnumerator FireLaser(Character user, List<Character> targets)
    {
        user.anim.PlayAttack(1);

        List<IEnumerator> laserCoroutines = new List<IEnumerator>();
        List<Coroutine> running = new List<Coroutine>();
        int finishedCount = 0;

        foreach (var target in targets)
        {
            if (target == null || target.Hp <= 0) continue;

            IEnumerator coroutine = FireSingleLaserWithNotify(user, target, () => finishedCount++);
            Coroutine handle = user.StartCoroutine(coroutine);
            running.Add(handle);
        }

        // Wait until all FireSingleLaserWithNotify calls complete
        yield return new WaitUntil(() => finishedCount >= running.Count);
    }


    private IEnumerator FireSingleLaserWithNotify(Character user, Character target, System.Action onComplete)
    {
        if (user == null || target == null)
        {
            onComplete?.Invoke();
            yield break;
        }

        GameObject laserInstance = Instantiate(laserPrefab);
        StartEndLaser laserScript = laserInstance.GetComponent<StartEndLaser>();

        float tickInterval = laserDuration / 3f;

        for (int i = 0; i < 3; i++)
        {
            float timer = 0f;

            while (timer < tickInterval)
            {
                // 유저나 타겟이 죽었을 경우 즉시 레이저를 파괴하고 종료
                if (user == null || user.Hp <= 0 || target == null || target.Hp <= 0)
                {
                    Destroy(laserInstance); // 레이저를 즉시 파괴
                    onComplete?.Invoke();
                    yield break;
                }

                Vector3 start = user.rightHand.position;
                Vector3 end = target.torso.position;
                Vector3 dir = (end - start).normalized;

                laserInstance.transform.position = start;
                laserInstance.transform.forward = dir;

                if (laserScript != null)
                    laserScript.MaxLength = Vector3.Distance(start, end);

                timer += Time.deltaTime;
                yield return null;
            }

            if (target != null && target.Hp > 0)
            {
                target.ApplyDamage(damage / 3f); // 3틱으로 나누어 데미지 적용
            }

            // 타겟이 죽었다면 반복문을 중지
            if (target == null || target.Hp <= 0)
                break;
        }

        // 레이저가 끝나면 처리
        if (laserScript != null)
        {
            laserScript.DisablePrepare();
        }

        Destroy(laserInstance, 1f); // 레이저 인스턴스는 여전히 1초 후 파괴될 수 있도록 설정
        onComplete?.Invoke();
    }

}
