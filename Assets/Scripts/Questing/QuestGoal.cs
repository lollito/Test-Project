using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal : MonoBehaviour
{
    public GoalType goalType;
    public Quest quest;
    public int requiredAmount;
    public int currentAmount;
    public bool completed = false;

    public void Evaluate()
    {
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        completed = true;
        quest.CheckGoals();
        Debug.Log("Goal marked as completed.");
    }

    void Start()
    {
        CombatEvents.OnEnemyDeath += EnemyDied;
    }

    public void EnemyDied() {
        if (goalType == GoalType.Kill) {
            currentAmount++;
        }
        Evaluate();
    }
}


public enum GoalType 
{
    Kill,
    Gathering
}