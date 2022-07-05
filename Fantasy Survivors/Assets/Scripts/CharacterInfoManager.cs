using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoManager : MonoBehaviour
{
    public static CharacterInfoManager instance;

    public Sprite characterSprite;
    public RuntimeAnimatorController animController;
    public GameObject startingAbility;
    public bool flipPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SetSprite(Sprite charSprite)
    {
        characterSprite = charSprite;
    }

    public void SetAnimController(RuntimeAnimatorController anim)
    {
        animController = anim;
    }

    public void SetStartingAbility(GameObject startAbility)
    {
        startingAbility = startAbility;
    }

    public void SetFlipPlayer(bool flip)
    {
        flipPlayer = flip;
    }

}
