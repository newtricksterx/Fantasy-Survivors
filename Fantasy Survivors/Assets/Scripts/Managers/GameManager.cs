using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // the player
    public GameObject player;

    public Text timeText;
    public FloatingTextManager floatingTextManager;
    
    // XP category
    public List<int> xpTable; 
    public int xpTableIndex;
    public ExperienceBar experienceBar;

    // Player attributes
    public int playerExperience;

    // Selectors
    public SelectorManager selector1;
    public SelectorManager selector2;
    public SelectorManager selector3;

    // AbilitySelectCanvas stuff
    public GameObject abilitySelectCanvas;

    // array/list of abilities as options already
    public List<GameObject> abilitiesPicked;

    // lootbox
    public GameObject lootboxPanel;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        xpTableIndex = 0;
        
        experienceBar.ResetExpBar();
        experienceBar.SetLevelText(xpTableIndex);
        experienceBar.SetMaxExp(xpTable[xpTableIndex]);

        abilitySelectCanvas.SetActive(false);

        player = GameObject.Find("Player");

        abilitiesPicked = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        timeText.text = (Mathf.Round(Time.time)).ToString();
    }

    public void ShowText(string msg, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontsize, color, position, motion, duration);
    }

    public void GrantXP(int experienceToAdd)
    {
        playerExperience += experienceToAdd;

        if(xpTableIndex < xpTable.Count - 1 && playerExperience >= xpTable[xpTableIndex])
        {
            xpTableIndex++;
            playerExperience = 0;
            OnLevelUp();
        }

        experienceBar.SetExp(playerExperience);
    }

    public void OnLevelUp()
    {
        //Debug.Log(xpTableIndex);
        experienceBar.SetMaxExp(xpTable[xpTableIndex]);
        experienceBar.SetLevelText(xpTableIndex);

        selector1.GetAbility();
        selector2.GetAbility();
        selector3.GetAbility();
        abilitySelectCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnSelection()
    {
        abilitySelectCanvas.SetActive(false);
        abilitiesPicked.Clear();
        Time.timeScale = 1f;
    }

    public void OnConfirm()
    {
        lootboxPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
