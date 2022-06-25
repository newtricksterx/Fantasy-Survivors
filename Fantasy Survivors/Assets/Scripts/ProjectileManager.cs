using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;

    private Transform player;

    // Pulse Projectile
    public GameObject projectilePulse;
    public float pulseCooldown = 1f;
    public bool canShootPulse = false;
    private float afterPulseShotTime;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = GameObject.Find("Player").transform;
        afterPulseShotTime = -pulseCooldown;
    }

    private void Update()
    {
        ShootPulseProjectile(player);
    }

    public void ShootPulseProjectile(Transform player)
    {
        if (GameObject.Find("Player") != null && canShootPulse && Time.time - pulseCooldown > afterPulseShotTime && GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && GameObject.FindObjectsOfType<ProjectilePulse>().Length == 0)
        {
            Instantiate(projectilePulse, player.position, Quaternion.identity);
            afterPulseShotTime = Time.time;
        }
        
    }
}
