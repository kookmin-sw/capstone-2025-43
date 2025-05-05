using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    Character character;
    public float wanderRadius = 5f;  
    public float wanderInterval = 3f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderInterval;
        character = GetComponent<Character>();
    }

    void Update()
    {
        if(character.Hp > 0)
        {
            timer += Time.deltaTime;

            if (timer >= wanderInterval)
            {
                Vector3 newDestination = GetRandomPointOnNavMesh(transform.position, wanderRadius);
                agent.SetDestination(newDestination);
                timer = 0f;
            }

        }
    }

    Vector3 GetRandomPointOnNavMesh(Vector3 center, float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return center;
    }
}
