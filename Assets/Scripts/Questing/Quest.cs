using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public bool isActive;

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;
    public bool completed = false;

    public List<QuestGoal> goals;

    public void CheckGoals()
    {
        completed = goals.All(g => g.completed);
        if (completed) {
            Complete();
        }
    }

    public void Complete() {
        isActive = false;
        Player.instance.PlayerLevel.GrantExperience(experienceReward);
    }
}
