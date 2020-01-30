using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    public static event System.Action OnEnemyDeath;

    public static void EnemyDied()
    {
        OnEnemyDeath?.Invoke();
    }
}