using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HUDUI : MonoBehaviour
{
    
    public Transform playerUI;
    public Image expSlider;
    Image healthSlider;

    public Text level;

    // Use this for initialization
    void Start()
    {
     
        
            
         healthSlider = playerUI.GetChild(2).GetChild(0).GetComponent<Image>();
          

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
        GetComponent<PlayerLevel>().OnLevelChanged += OnLevelChanged;
        GetComponent<PlayerLevel>().OnExperienceChanged += OnExperienceChanged;
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (playerUI != null)
        {
           

            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            
        }
    }

    void OnExperienceChanged(int requiredExperience, int currentExperience)
    {
        if (playerUI != null)
        {


            float expPercent = (float)currentExperience / requiredExperience;
            expSlider.fillAmount = expPercent;

        }
    }

    void OnLevelChanged(string value)
    {
        if (level != null)
        {

            level.text = value;
           
        }
    }

}
