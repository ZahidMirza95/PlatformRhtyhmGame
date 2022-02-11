using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health;
    private float maxHealth;
    public Component particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(),GetComponent<BoxCollider2D>());
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Instantiate(particleSystem, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
