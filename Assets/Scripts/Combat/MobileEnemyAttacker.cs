using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Combat
{
    public class MobileEnemyAttacker : MonoBehaviour
    {
        [SerializeField] Transform target;
        //[SerializeField] Transform patrolRoute;

        NavMeshAgent myNavMeshAgent;

        private void Awake() {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            myNavMeshAgent.SetDestination(target.position);
        }
    }
}


