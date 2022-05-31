using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider enemySlider;

    public void SetEnemyMaxHealth(int health)
    {
        enemySlider.maxValue = health;
        enemySlider.value = health;
    }

    public void SetEnemyHealth(int health)
    {
        enemySlider.value = health;
    }
}
