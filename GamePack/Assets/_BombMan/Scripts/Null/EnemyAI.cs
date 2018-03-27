using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// 根据原作者Foldcc基础，进行修改添加
/// </summary>
public enum monState
{
    triggerDefault,
    triggerWallBomb,
    triggerBomb,
}
//触发到火焰判断
public enum monFire
{
    monFireDefault,
    monFireOpen,
    monFireClose
}
public class EnemyAI : MonoBehaviour
{
    public static EnemyAI _instance;
    /// <summary>
    /// 怪物状态:穿墙，穿雷
    /// </summary>
    /// 
    public monState monController;
    public monFire monFireState;

    private Vector2 moveVector;
    private Rigidbody2D rod;
    private int moveingID = 0;
    public float speed;
    public float weakTime;
    // Use this for initialization
    void Start()
    {
        _instance = this;
        rod = this.GetComponent<Rigidbody2D>();
        randomMove();
        InvokeRepeating("randomMove", 3, weakTime);
        //transform.GetComponent<CircleCollider2D>().radius = 0;
        monFireState = monFire.monFireClose;
        StartCoroutine(monStateFire(1f));
    }
    IEnumerator monStateFire(float time)
    {
        yield return new WaitForSeconds(time);
        monFireState = monFire.monFireOpen;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rod.MovePosition((Vector2)transform.position + moveVector * speed * Time.deltaTime);
    }

    void randomMove(int[] randoms)
    {
        moveingID = Random.Range(0, randoms.Length);
        if (randoms[moveingID] == 0)
        {
            moveVector = Vector2.up;
        }
        else if (randoms[moveingID] == 1)
        {
            moveVector = Vector2.down;
        }
        else if (randoms[moveingID] == 2)
        {
            moveVector = Vector2.left;
        }
        else
        {
            moveVector = Vector2.right;
        }
    }

    void randomMove()
    {
        moveingID = Random.Range(0, 4);
        if (moveingID == 0)
        {
            moveVector = Vector2.up;
        }
        else if (moveingID == 1)
        {
            moveVector = Vector2.down;
        }
        else if (moveingID == 2)
        {
            moveVector = Vector2.left;
        }
        else
        {
            moveVector = Vector2.right;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Boom"))
        {
            //复位
            this.transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            if (moveingID == 0)
            {
                randomMove(new int[] { 1, 2, 3 });
            }
            else if (moveingID == 1)
            {
                randomMove(new int[] { 0, 2, 3 });
            }
            else if (moveingID == 2)
            {
                randomMove(new int[] { 1, 0, 3 });
            }
            else if (moveingID == 3)
            {
                randomMove(new int[] { 1, 2, 0 });
            }
        }
        else if (other.CompareTag("Boom")&&monFire.monFireOpen== monFireState)
        {
 
            UIController.enemyCount -= 1;
            if (UIController.enemyCount <= 0)
            { 
                UIController.enemyCount = 0;
                Player._instance.playBeatTheGameAudio();
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameContorller>().enemyCount -= 1;
            Destroy(this.gameObject);
        }
        if(other.CompareTag("WallBomb"))
        {
            if(monState.triggerWallBomb== monController)
            {
                other.transform.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            
        }
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            Instantiate(Resources.Load("Prefabs/playerDead"), other.transform.position, Quaternion.identity);
            GameContorller._instance.gameOver();

        }
    }
}