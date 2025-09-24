/*
Originial Coder: Owynn A.
Recent Coder: Owynn A.
Recent Changes: Initial Coding
Last date worked on: 9/23/2025
*/

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GhostBehaviour : MonoBehaviour
{
    public Animator animator;
    public float radius = 10f;
    public float waitTime = 2f;
    private NavMeshAgent agent;
    public bool isWandering = true;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(WanderRoutine());
    }
    private IEnumerator WanderRoutine()
    {
        while (isWandering)
        {
            Vector3 destination = GetRandomNavMeshPoint(transform.position, radius);
            agent.SetDestination(destination);
            Walk();
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            Idle();
            
            yield return new WaitForSeconds(waitTime);
        }
    }
    private Vector3 GetRandomNavMeshPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPos = center + Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center;
    }
    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void Walk()
    {
        animator.SetBool("isWalking", true);
    }

    public void Idle()
    {
        animator.SetBool("isWalking", false);
    }

    public void Disappear()
    {
        animator.SetTrigger("disappear");
    }

    public void Appear()
    {
        
    }

    public void TakeDamage()
    {
        
    }
}
