using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireballs : Ability
{
    public override void LevelEffect()
    {
        RotatingFireball[] childrens = GetComponentsInChildren<RotatingFireball>();

        foreach (RotatingFireball rf in childrens)
        {
            rf.rotateSpeed = levelEffect[AbilitiesManager.instance.abilitiesToLevels[gameObject]];
        }
    }

    protected override void Start()
    {
    }

    protected override void Update()
    {
    }
}
