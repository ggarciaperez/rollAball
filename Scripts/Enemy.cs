using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private NavMeshAgent pathfinder;
    private Transform target;
    
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        //goes after the game object Player
        target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        pathfinder.SetDestination(target.position);
        //Debug.Log(target.position);
    }
}