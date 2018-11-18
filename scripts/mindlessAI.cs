using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Not turn based?

public class mindlessAI : MonoBehaviour
{
    movement moveScript;

    public bool isBoss;
    public int knockback;

    public int maxHP;
    public int currentHP;

    public LayerMask wall;
    public LayerMask interactable;
    public LayerMask player;

    public bool chase;
    public float chaseDistance;
    
    int count = 0;
    public int moveWait;

    // Smack a bitch
    public Transform target;
    public float attackRange;
    public int meleeDamage;
    private float lastAttackTime;
    public float attackDelay;
    

    public bool copperAdd;
    public bool silverAdd;
    public bool goldAdd;
    public bool platinumAdd;

    //int acount;
    //int bcount;
    //int ccount;
    //int dcount;
    //int ecount;

    private void Start()
    {

        moveScript = GetComponent<movement>();
    }



    public void EnemyTurn()
    {
        //Debug.Log("Enemy Turn");

        int ran = Random.Range(0, 4);
        count++;

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer < attackRange)
        {
            if (Time.time > lastAttackTime + attackDelay)
            {
                Debug.Log(meleeDamage);
                movement.instance.TakeDamage(meleeDamage, isBoss, knockback);

                lastAttackTime = Time.time;
            }
        }


        if (!isClose(chaseDistance) || (isClose(chaseDistance) && !chase))
        {
            count = 0;

            if (isUpBlocked() == 0 && ran == 0)
            {

                gameObject.transform.Translate(0, 1f, 0);
                //acount++;
                //Debug.Log(ran);
            }

            if (isLeftBlocked() == 0 && ran == 2)
            {

                gameObject.transform.Translate(-1f, 0, 0);
                //bcount++;
                //Debug.Log(ran + " : " + bcount);
            }
            if (isDownBlocked() == 0 && ran == 1)
            {

                gameObject.transform.Translate(0, -1f, 0);
                //ccount++;
                //Debug.Log(ran + " : " + ccount);

            }
            if (isRightBlocked() == 0 && ran == 3)
            {


                gameObject.transform.Translate(1f, 0, 0);
                //dcount++;
                //Debug.Log(ran + " : " + dcount);
            }
            else if (ran == 2)
            {
                gameObject.transform.Translate(0, 0, 0);
                //ecount++;
                //Debug.Log(ran + " : " + ecount);
            }
        }
        else if (isUpBlocked() != 3 && isDownBlocked() != 3 && isRightBlocked() != 3 && isLeftBlocked() != 3 && chase)
        {
            count = 0;
            gameObject.transform.Translate(getMove());
            Debug.Log(getMove());
        }
        else
        {
        }



        //Debug.Log(ran);


        //Debug.Log(getDirection());
        //Debug.Log(getMove());
        //Debug.Log("Close? " + isClose(chaseDistance));
    }










    // 0 = nothing 1 = wall 2 = interactable 3 = enemy

    int isDownBlocked()
    {
        Vector2 position = transform.position;
        Vector2 downdir = (Vector2.down) * 1f;
        float distance = 1f;

        Debug.DrawRay(position, downdir, Color.red);
        RaycastHit2D block = Physics2D.Raycast(position, downdir, distance, wall);
        RaycastHit2D inter = Physics2D.Raycast(position, downdir, distance, interactable);
        RaycastHit2D p = Physics2D.Raycast(position, downdir, distance, player);

        if (block)
        {
            //Debug.Log("1D");
            return 1;
        }
        if (inter)
        {
            //Debug.Log(inter.collider.name);
            return 2;
        }
        if (p)
        {
            Debug.Log("P");
            return 3;
        }

        return 0;
    }

    int isUpBlocked()
    {
        Vector2 position = transform.position;
        Vector2 updir = (Vector2.up) * 1f;
        float distance = 1f;

        Debug.DrawRay(position, updir, Color.red);
        RaycastHit2D block = Physics2D.Raycast(position, updir, distance, wall);
        RaycastHit2D inter = Physics2D.Raycast(position, updir, distance, interactable);
        RaycastHit2D p = Physics2D.Raycast(position, updir, distance, player);

        if (block)
        {
            //Debug.Log("1U");
            return 1;
        }
        if (inter)
        {
            //Debug.Log(inter.collider.name);
            return 2;
        }
        if (p)
        {
            Debug.Log("P");
            return 3;
        }

        return 0;
    }

    int isLeftBlocked()
    {
        Vector2 position = transform.position;
        Vector2 leftdir = (Vector2.left) * 1f;
        float distance = 1f;

        Debug.DrawRay(position, leftdir, Color.red);
        RaycastHit2D block = Physics2D.Raycast(position, leftdir, distance, wall);
        RaycastHit2D inter = Physics2D.Raycast(position, leftdir, distance, interactable);
        RaycastHit2D p = Physics2D.Raycast(position, leftdir, distance, player);

        if (block)
        {
            //Debug.Log("1L");
            return 1;
        }
        if (inter)
        {
            //Debug.Log(inter.collider.name);
            return 2;
        }
        if (p)
        {
            Debug.Log("P");
            return 3;
        }

        return 0;
    }

    int isRightBlocked()
    {
        Vector2 position = transform.position;
        Vector2 rightdir = (Vector2.right) * 1f;
        float distance = 1f;

        Debug.DrawRay(position, rightdir, Color.red);
        RaycastHit2D block = Physics2D.Raycast(position, rightdir, distance, wall);
        RaycastHit2D inter = Physics2D.Raycast(position, rightdir, distance, interactable);
        RaycastHit2D p = Physics2D.Raycast(position, rightdir, distance, player);

        if (block)
        {
            //Debug.Log("1R");
            return 1;
        }
        if (inter)
        {
            //Debug.Log(inter.collider.name);
            return 2;
        }
        if (p)
        {
            Debug.Log("P");
            return 3;
        }

        return 0;
    }




    public void takeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            
            if (copperAdd)
            {
                movement.instance.copperAdd();
            }
            if (silverAdd)
            {
                movement.instance.silverAdd();
            }
            if (goldAdd)
            {
                movement.instance.goldAdd();
            }
            if (platinumAdd)
            {
                movement.instance.platinumAdd();
            }
            Destroy(gameObject);
        }
    }

    Vector3 getDirection()
    {
        return (gameObject.transform.position - movement.instance.playerLoc());
    }
    bool isClose(float chaseDistance)
    {
        if ((getDirection().x < chaseDistance && getDirection().x > -chaseDistance)
            && (getDirection().y < chaseDistance && getDirection().y > -chaseDistance))
        {
            return true;
        }
        if ((getDirection().x == 1 || getDirection().x == -1)
            && (getDirection().y == 1 || getDirection().y == -1))
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    Vector3 getMove()
    {
        Vector3 mov = Vector3.zero;

        if (getDirection().y < 0 && isUpBlocked() == 0)
        {
            mov.y = 1;
            return mov;
        }
        if (getDirection().y > 0 && isDownBlocked() == 0)
        {
            mov.y = -1;
            return mov;
        }

        if (getDirection().x < 0 && isRightBlocked() == 0)
        {
            mov.x = 1;
            return mov;
        }
        if (getDirection().x > 0 && isLeftBlocked() == 0)
        {
            mov.x = -1;
            return mov;
        }

        return Vector3.zero;

    }









}