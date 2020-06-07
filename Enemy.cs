using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    private bool isAlive;

    public float startHealth = 100f;
    private float health;


    public int worthMoney = 50;
    public int worthScore = 10;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        isAlive = true;
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0)
        {
            if(isAlive)
                Die();
        }
    }
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isAlive = false;
        PlayerStats.Money += worthMoney;
        PlayerStats.Score += worthScore;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);

    }
}
