using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    //Method 1: Creating a UNIQUE distance between each enemy
    [SerializeField] Vector2[] distances;
    //pos is used by both Method 1 and Method 2 (starting position of the first enemy)
    [SerializeField] Vector2 pos;
    //Method 2: Creating a repetitive distance between each enemy. They're seperated the same amount of distance
    [SerializeField] bool repetitive;
    [SerializeField] Vector2 repetitiveDistance;
    [SerializeField] int numEnemies;
    GameObject player;
    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if(player.GetComponent<Rigidbody2D>().position.x == 5)
        {
            SpawnEnemies(enemy, distances, pos);
        }*/
    }

    //Spawns enemies a specified distance away from each other
    //Only spawns enemies up to the number of entries in distances
    //Also need to specify a starting position that the enemy spawns at
    public void SpawnEnemies(GameObject enemy, Vector2[] distances, Vector2 pos) 
    {
        if(!repetitive)
        {
            foreach (Vector2 distance in distances)
            {
                //Adds enemies to the current chain while also creating them
                //A chain is just a chain of enemies that, when you defeat all of them up until the last one
                //Your attack changes to a finisher to finish that final enemy off
                player.GetComponent<PlayerCombat>().AddEnemiesToChain(Instantiate(enemy, pos + distance, Quaternion.identity));
                pos += distance;
            }
        }
        
        else
        {
            for(int i = 0; i < numEnemies; i++)
            {
                player.GetComponent<PlayerCombat>().AddEnemiesToChain(Instantiate(enemy, pos + repetitiveDistance, Quaternion.identity));
                pos += repetitiveDistance;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player collides with the enemy spawner, spawn the enemies
        if(collision.tag == "Player")
        {
            if(player.GetComponent<PlayerCombat>().EnemiesInChain())
            {
                player.GetComponent<PlayerCombat>().RemoveAllEnemiesInChain();
            }
            SpawnEnemies(enemy, distances, boxCollider2D.offset + pos);
            Destroy(this.gameObject);
        }
    }
}
