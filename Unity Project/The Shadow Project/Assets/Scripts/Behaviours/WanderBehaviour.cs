/*
Originial Coder: Owynn A.
Recent Coder: Owynn A.
Recent Changes: Initial Coding
Last date worked on: 9/23/2025
*/

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class WanderBehaviour : MonoBehaviour
{
    public float radius = 10f;
    public float waitTime = 2f;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(WanderRoutine());
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            Vector3 destination = GetRandomNavMeshPoint(transform.position, radius);
            agent.SetDestination(destination);

            // Wait until the agent reaches the destination
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;  // Wait until next frame
            }

            // Wait at destination for a bit
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 GetRandomNavMeshPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 30; i++) // Try multiple times to find a valid point
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center; // Fallback if no valid point found
    }
}