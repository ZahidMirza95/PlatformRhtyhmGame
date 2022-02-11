using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;
    [SerializeField] LayerMask enemyLayerMask;
    [SerializeField] GameObject target;
    List<GameObject> currentChain;
    public GameObject comboCounterUI;
    ComboTextController comboTextController;
    bool enemyInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentChain = new List<GameObject>();
        comboTextController = comboCounterUI.GetComponent<ComboTextController>();
    }

    // Update is called once per frame
    void Update()
    {
        //PlaceTarget();
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(currentChain.Count == 1)
            {
                animator.SetTrigger("finalAttack");
            }
            else
            {
                animator.SetTrigger("leftPunch");
            }
            Attack();
        }
        //Check if a target can be placed on the enemy
        PlaceTarget();
        //Debug.Log(targetCast.collider != null);
    }

    void PlaceTarget()
    {
        //Simple target placing where only enemies in sight have a target placed on them (might be harder to calculate combos)
        /*RaycastHit2D targetCast = Physics2D.Raycast(rigidbody2D.position, Vector2.right, 1.0f, enemyLayerMask);
        Debug.DrawLine(rigidbody2D.position, rigidbody2D.position + Vector2.right * 1.0f, Color.green);
        //If an enemy is in sight
        //Target placing code
        if (targetCast.collider != null)
        {
            Vector2 enemyPosition = targetCast.collider.gameObject.GetComponent<Rigidbody2D>().position;
            target.transform.position = enemyPosition;
            //targe
        }
        else
        {
            target.transform.position = new Vector2(-1000, -1000);
        }*/
        //Make it so that a box is created instead, so any enemies that pass by the player, regardless of their height are taken into account
        Collider2D targetCollider = Physics2D.OverlapBox(rigidbody2D.position,new Vector2(2.0f,100.0f),0.0f,enemyLayerMask);
        if (targetCollider != null)
        {
            Vector2 enemyPosition = targetCollider.gameObject.GetComponent<Rigidbody2D>().position;
            target.transform.position = enemyPosition;
            enemyInRange = true;
            //targe
        }
        else
        {
            //If enemy was in range before but the player failed to kill them, reset the combo counter
            if(enemyInRange)
            {
                comboTextController.setComboCounter(0);
            }
            enemyInRange = false;
            target.transform.position = new Vector2(-1000, -1000);
        }
    }

    void Attack()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(rigidbody2D.position + Vector2.right*0.7f, 0.5f, enemyLayerMask);
        foreach(Collider2D enemy in enemiesInRange)
        {
            //Enemy takes 5 points of damage
            enemy.GetComponent<Enemy>().setHealth(5.0f);
            //Remove enemy from the current chain if it got attacked
            currentChain.Remove(enemy.gameObject);
            /*if (currentChain.Count == 0)
            {
                //enemySpawner.IncrementChainIndex();
            }*/
            comboTextController.setComboCounter(comboTextController.getComboCounter()+1);
            enemyInRange = false;
        }
    }

    public void AddEnemiesToChain(GameObject enemy)
    {
        currentChain.Add(enemy);
    }

    public void RemoveAllEnemiesInChain()
    {
        currentChain = new List<GameObject>();
    }

    public bool EnemiesInChain()
    {
        return currentChain.Count > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rigidbody2D.position + Vector2.right * 0.5f, 0.5f);
        Gizmos.DrawWireCube(rigidbody2D.position, new Vector2(2.0f, 100.0f));
        //Gizmos.DrawLine(rigidbody2D.position, rigidbody2D.position + Vector2.right * 5.0f);
    }
}
