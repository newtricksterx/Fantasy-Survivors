using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Ability
{
    public float armorValue;


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
        armorValue = levelEffect[AbilitiesManager.instance.abilitiesToLevels[gameObject]];
    }
}
