using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : Collidable
{
    public string abilityName;
    public List<float> levelEffect;


    public virtual void LevelEffect()
    {
        // do stuff on level up
    }
}