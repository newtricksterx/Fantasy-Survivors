using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : Collidable
{
    public List<float> levelEffect;  

    public virtual void LevelEffect()
    {
        // do stuff on level up
    }
}
