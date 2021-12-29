using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration", order = 0)]
public class EnemyScriptableObject : ScriptableObject 
{
    // Enemy Stats
    public int health = 100;

    // NavMeshAgent Configs
    public float aiUpdateInterval = 0.1f;

    public float acceleration = 8f;
    public float angularSpeed = 120f;
    // -1 means everything
    public int areaMask = -1;
    public int avoidancePriority = 50;
    public float baseOffset = 0f;
    public float height = 2f;
    public ObstacleAvoidanceType obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float radius = 0.5f;
    public float speed = 3f;
    public float stoppingDistance = 0.5f;
}
