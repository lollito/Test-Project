using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int Level { get; set; }
    public int CurrentExperience { get; set; }
    public int RequiredExperience { get { return Level * 25; } }

    public event System.Action<string> OnLevelChanged;
    public event System.Action<int, int> OnExperienceChanged;

    // Use this for initialization
    void Start()
    {
        
        Level = 1;
    }

    public void GrantExperience(int amount)
    {
        CurrentExperience += amount;
        print("CurrentExperience: " + CurrentExperience);
        print("RequiredExperience: " + RequiredExperience);
        while (CurrentExperience >= RequiredExperience)
        {
            CurrentExperience -= RequiredExperience;
            Level++;
            OnLevelChanged?.Invoke(Level.ToString());
        }
        OnExperienceChanged?.Invoke(RequiredExperience, CurrentExperience);
    }
}
