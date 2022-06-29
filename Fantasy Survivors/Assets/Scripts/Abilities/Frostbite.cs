using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frostbite : Ability
{
    public int numberOfEnemiesToFreeze;
    public float durationOfFreeze;
    public float freezeCooldown;
    public GameObject iceCocoon;

    private float frozenTime;
    private Enemy[] listOfEnemies;
    private List<int> indexOfEnemiesFrozen;

    // Start is called before the first frame update
    protected override void Start()
    {
        frozenTime = -freezeCooldown;
        indexOfEnemiesFrozen = new List<int>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Time.time - freezeCooldown >= frozenTime && FindObjectsOfType<Enemy>().Length > 0)
        {
            Freeze();
            frozenTime = Time.time;
        }
    }

    public override void LevelEffect()
    {
        numberOfEnemiesToFreeze = Mathf.RoundToInt(levelEffect[AbilitiesManager.instance.abilitiesToLevels[gameObject]]);
    }

    private Enemy GetRandomEnemy()
    {
        listOfEnemies = FindObjectsOfType<Enemy>();

        int index = Random.Range(0, listOfEnemies.Length);

        // we need to make sure that the number of enemies that we can freeze is <= to the number of enemies actually existing in the game

        while (indexOfEnemiesFrozen.Contains(index))
        {
            index = Random.Range(0, listOfEnemies.Length);
        }

        return listOfEnemies[index];
    }

    private void Freeze()
    {
        int numberOfEnemies = FindObjectsOfType<Enemy>().Length;

        int actualNumberOfEnemiesToFreeze = numberOfEnemiesToFreeze;

        if(numberOfEnemies < numberOfEnemiesToFreeze)
        {
            actualNumberOfEnemiesToFreeze = numberOfEnemies;
        }

        for (int i = 0; i < actualNumberOfEnemiesToFreeze; i++)
        {
            Enemy randomEnemy = GetRandomEnemy();

            //Debug.Log("hit");
            StartCoroutine(FreezeEnemy(randomEnemy));
        }
    }

    IEnumerator FreezeEnemy(Enemy enemyToFreeze)
    {
        float originalSpeed = enemyToFreeze.movementSpeed;
        enemyToFreeze.movementSpeed = 0;

        GameObject instance = Instantiate(iceCocoon, enemyToFreeze.gameObject.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(durationOfFreeze);

        enemyToFreeze.movementSpeed = originalSpeed;
        Destroy(instance);
    }
}
