using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public static movement instance;

    public int turnpause = 0;
    int count = 0;

    public int maxHP = 50;
    public int currentHP;
    public int gold = 0;

    // Gold Types
    int copperCoin = 100;
    int silverCoin = 1000;
    int goldCoin = 10000;
    int platinumCoin = 1000000;

    public GameObject front;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject back;

    Transform transf;
    public LayerMask wall;
    public LayerMask interactable;
    public LayerMask enemy;

    public GameObject axRot;
    public int axeStrength = 1;
    Damageable damageableComponent;
    public int pickupheal = 10;

    public List<GameObject> enemys;

    mindlessAI targ;
    chest ctarg;
    hpickup htarg;

    // Use this for initialization
    void Start()
    {
        instance = this;
        transf = gameObject.GetComponent<Transform>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("V : " + Input.GetAxis("Vertical"));
        Debug.Log("H : " + Input.GetAxis("Horizontal"));
        count++;
        if (count >= turnpause)
        {
            if (isUpBlocked() == 0 && Input.GetAxis("Vertical") == 1)
            {
                count = 0;
                transf.Translate(0, 1f, 0);
                front.SetActive(false);
                leftSide.SetActive(false);
                rightSide.SetActive(false);
                back.SetActive(true);
                foreach (GameObject go in enemys)
                {
                    mindlessAI baddy = go.gameObject.GetComponent<mindlessAI>();
                    if (baddy)
                    {
                        baddy.EnemyTurn();
                    }
                }
            }

            if (isLeftBlocked() == 0 && Input.GetAxis("Horizontal") == -1)
            {
                count = 0;
                transf.Translate(-1f, 0, 0);
                front.SetActive(false);
                leftSide.SetActive(true);
                rightSide.SetActive(false);
                back.SetActive(false);
                foreach (GameObject go in enemys)
                {
                    mindlessAI baddy = go.gameObject.GetComponent<mindlessAI>();
                    if (baddy)
                    {
                        baddy.EnemyTurn();
                    }
                    else
                    {
                        count = 0;
                        return;
                    }
                }
            }

            if (isDownBlocked() == 0 && Input.GetAxis("Vertical") == -1)
            {
                count = 0;
                transf.Translate(0, -1f, 0);
                front.SetActive(true);
                leftSide.SetActive(false);
                rightSide.SetActive(false);
                back.SetActive(false);
                foreach (GameObject go in enemys)
                {
                    mindlessAI baddy = go.gameObject.GetComponent<mindlessAI>();
                    if (baddy)
                    {
                        baddy.EnemyTurn();
                    }
                }
            }

            if (isRightBlocked() == 0 && Input.GetAxis("Horizontal") == 1)
            {
                count = 0;
                transf.Translate(1f, 0, 0);
                front.SetActive(false);
                leftSide.SetActive(false);
                rightSide.SetActive(true);
                back.SetActive(false);
                foreach (GameObject go in enemys)
                {
                    mindlessAI baddy = go.gameObject.GetComponent<mindlessAI>();
                    if (baddy)
                    {
                        baddy.EnemyTurn();
                    }
                }
            }

            if (Input.GetButtonDown("SkipTurn"))
            {
                count = 0;
                foreach (GameObject go in enemys)
                {
                    mindlessAI baddy = go.gameObject.GetComponent<mindlessAI>();
                    if (baddy)
                    {
                        baddy.EnemyTurn();
                    }
                }
            }
            if ((isRightBlocked() == 2 || isUpBlocked() == 2 || isDownBlocked() == 2 || isLeftBlocked() == 2) && Input.GetButtonDown("Interact"))
            {
                count = 0;
                if (ctarg)
                {
                    ctarg.Open();
                }
                if (htarg)
                {
                    Heal();
                    Destroy(htarg.gameObject);
                }

                foreach (GameObject go in enemys)
                {
                    mindlessAI baddy = go.gameObject.GetComponent<mindlessAI>();
                    if (baddy)
                    {
                        baddy.EnemyTurn();
                    }
                    else
                    {
                        count = 0;
                        return;
                    }
                }
            }


            if (Input.GetButtonDown("Atack"))
            {
                count = 0;
                axRot.SetActive(true);
                axRot.transform.Rotate(0, 0, -3);
                if ((isRightBlocked() == 3 || isUpBlocked() == 3 || isDownBlocked() == 3 || isLeftBlocked() == 3))
                {
                    targ.takeDamage(axeStrength);
                    Debug.Log("Hit");
                }

                foreach (GameObject go in enemys)
                {
                    mindlessAI baddy = go.gameObject.GetComponent<mindlessAI>();
                    if (baddy)
                    {
                        baddy.EnemyTurn();
                    }
                    else
                    {
                        count = 0;
                        return;
                    }
                }
            }
            else
            {

                axRot.SetActive(false);
                axRot.transform.rotation = Quaternion.Euler(0, 0, 15);
            }

            LoadScript.instance.Wealth(gold);
            LoadScript.instance.Health(currentHP);

            count = 0;
        }
        // THIS IS WHAT WE SHOULD DO FOR MOVEMENT WITH GETAXIS

        //transf.Transltate(Input.GetAxis("Horizontal"), Input.GetAxis("Horizontal"), 0);

        // Than set inputs to snapto, so they are always 1, 0, or -1. This will also
        // add horizontal movement in our final build.

        // all movement in one line.


        
    }

    public void copperAdd()
    {
        gold += copperCoin;
    }

    public void silverAdd()
    {
        gold += silverCoin;
    }

    public void goldAdd()
    {
        gold += goldCoin;
    }

    public void platinumAdd()
    {
        gold += platinumCoin;
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
        RaycastHit2D baddy = Physics2D.Raycast(position, downdir, distance, enemy);

        if (block)
        {
            //Debug.Log("1D");
            return 1;
        }
        if (inter)
        {
            //Debug.Log(inter.collider.name);
            ctarg = inter.collider.GetComponent<chest>();
            htarg = inter.collider.GetComponent<hpickup>();
            return 2;
        }
        if (baddy)
        {
            targ = baddy.collider.GetComponent<mindlessAI>();
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
        RaycastHit2D baddy = Physics2D.Raycast(position, updir, distance, enemy);

        if (block)
        {
            //Debug.Log("1U");
            return 1;
        }
        if (inter)
        {
            //Debug.Log(inter.collider.name);
            ctarg = inter.collider.GetComponent<chest>();
            htarg = inter.collider.GetComponent<hpickup>();
            return 2;
        }
        if (baddy)
        {
            targ = baddy.collider.GetComponent<mindlessAI>();
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
        RaycastHit2D baddy = Physics2D.Raycast(position, leftdir, distance, enemy);

        if (block)
        {
            //Debug.Log("1L");
            return 1;
        }
        if (inter)
        {
            //Debug.Log(inter.collider.name);
            ctarg = inter.collider.GetComponent<chest>();
            htarg = inter.collider.GetComponent<hpickup>();
            return 2;
        }
        if (baddy)
        {
            targ = baddy.collider.GetComponent<mindlessAI>();
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
        RaycastHit2D baddy = Physics2D.Raycast(position, rightdir, distance, enemy);

        if (block)
        {
            //Debug.Log("1R");
            return 1;
        }
        if (inter)
        {
            //Debug.Log(inter.collider.name);
            ctarg = inter.collider.GetComponent<chest>();
            htarg = inter.collider.GetComponent<hpickup>();
            return 2;
        }
        if (baddy)
        {
            targ = baddy.collider.GetComponent<mindlessAI>();
            return 3;
        }

        return 0;
    }

    public Vector3 playerLoc()
    {
        return transf.position;
    }

    public int Treasure()
    {
        return gold;
    }

    public void TakeDamage(int damage, bool isBoss, int knockback)
    {
        currentHP -= damage;
        Debug.Log(damage);
        if(isBoss)
        {
            Debug.Log("Boss Hit");
            if (isRightBlocked() == 3)
            {
                transf.Translate(knockback, 0, 0);
            }
            if (isUpBlocked() == 3)
            {
                transf.Translate(0, knockback, 0);
            }
            if (isDownBlocked() == 3)
            {
                transf.Translate(0, -knockback, 0);
            }
            if (isLeftBlocked() == 3)
            {
                transf.Translate(-knockback, 0, 0);
            }
        }
        
        if (currentHP <= 0)
        {
            Debug.Log("Dead!");
        }
    }


    public void Heal()
    {
        int chp = currentHP;
        while (currentHP < (chp + pickupheal) && currentHP < maxHP)
        {
            currentHP++;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        warp pad = other.gameObject.GetComponent<warp>();
        if (pad)
        {
            Debug.Log("Town");
            LoadScript.instance.LoadTown();
        }
    }
}