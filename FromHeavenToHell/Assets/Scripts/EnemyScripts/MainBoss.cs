﻿using UnityEngine;

public class MainBoss : EnemyBaseClass
{
    [SerializeField] private Ability[] abilityList;
    [SerializeField] private float switchingAbilityInterval;
    private float counter;


    void Awake()
    {
        Ability = abilityList[Random.Range(0, abilityList.Length)];
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameManager.instance.Paused == false)
        {
            counter += Time.deltaTime;
            if (counter > switchingAbilityInterval)
            {
                Ability = abilityList[Random.Range(0, abilityList.Length)];
                counter = 0;
            }
        }
    }
}
