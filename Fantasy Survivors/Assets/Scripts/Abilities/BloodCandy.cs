using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCandy : Ability
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void LevelEffect()
    {
        GameManager.instance.player.GetComponent<Player>().maxHP = levelEffect[AbilitiesManager.instance.abilitiesToLevels[gameObject]];
    }
}
