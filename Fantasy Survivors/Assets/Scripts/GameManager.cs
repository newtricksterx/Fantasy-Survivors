using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text timeText;
    public FloatingTextManager floatingTextManager;
    
    // XP category
    public List<int> xpTable; 
    public int xpTableIndex;
    public ExperienceBar experienceBar;

    // Player attributes
    public int playerExperience;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        xpTableIndex = 0;
        
        experienceBar.ResetExpBar();
        experienceBar.SetLevelText(xpTableIndex);
        experienceBar.SetMaxExp(xpTable[xpTableIndex]);
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

        if(playerExperience >= xpTable[xpTableIndex] && xpTableIndex < xpTable.Count)
        {
            xpTableIndex++;
            playerExperience = 0;
            OnLevelUp();
        }

        experienceBar.SetExp(playerExperience);
    }

    public void OnLevelUp()
    {
        experienceBar.SetMaxExp(xpTable[xpTableIndex]);
        experienceBar.SetLevelText(xpTableIndex);
    }
}
