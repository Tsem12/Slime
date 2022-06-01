using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private bool canMooveBar;
    [SerializeField] private bool canDisableBar = true;
    [SerializeField] private Transform mob;
    [SerializeField] float yOffSet;
    public Slider slider;

    private void Update()
    {
        if (canMooveBar && mob != null)
        {
            this.transform.position = new Vector3(mob.position.x, mob.position.y + yOffSet, mob.position.z);
        }

        if(slider.value <= 0f && canDisableBar)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    public void SetMawHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
