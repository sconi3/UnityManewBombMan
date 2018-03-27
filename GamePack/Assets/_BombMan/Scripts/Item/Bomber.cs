using UnityEngine;
using System.Collections;

public class Bomber : MonoBehaviour
{      /// <summary>
    /// 炸弹长度单例模式
    /// </summary>
    public static Bomber _instance;
    public int bombDistance;
     

    GameObject Itemfire;
    bool iswallUp = false;
    bool iswallDown = false;
    bool iswallLeft = false;
    bool iswallRight = false;

    private int wallmask;
    GameObject ItemfireU;
    GameObject ItemfireD;
    GameObject ItemfireL;
    GameObject ItemfireR;

    //炸弹音效

    // Use this for initialization
    void Start()
    {
        GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/pressBomb", typeof(AudioClip));
        GetComponent<AudioSource>().Play();
        _instance = this;
        wallmask = LayerMask.GetMask("Wall");
        bombDistance = Player._instance.bombHeight;
        //炸弹状态 
        if(bombState.timeBomb==Player._instance.bombController)
        {
            Player._instance.bombTime = bombState.closeBomb; 
        }
        else
        {
            Player._instance.bombTime = bombState.openBomb; 
        }
        Player._instance.bombNum--;
    }

    // Update is called once per frame
    void Update()
    {
        //炸弹属于开启状态以及不是遥控炸弹状态，两个条件必须都满足
        if (bombState.openBomb == Player._instance.bombTime && bombState.timeBomb != Player._instance.bombTime)
        { 
            StartCoroutine(bomboo(2.5f));
        }
        //炸弹属于开启状态以及是遥控炸弹状态，两个条件必须都满足
        if (bombState.openBomb == Player._instance.bombTime && bombState.timeBomb == Player._instance.bombController)
        { 
            StartCoroutine(bomboo(0.1f));
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boom")
        {
            StartCoroutine(bomboo(0.1f));
        }
    }
    IEnumerator bomboo(float time)
    {
        yield return new WaitForSeconds(time);
 
        Player._instance.bombNum++;
        //爆炸后放雷状态开启,这个状态的改变也没啥必要，因为炸一下主角就挂了，也不可能再放炸弹了，但是为了系统的完善，就加上了这个状态的改变
        Player._instance._KeyPress = playerState._KEYOPEN;
        //删除炸弹自身
        Destroy(gameObject);
        Itemfire = Instantiate(Resources.Load("Prefabs/fire"), transform.position, transform.rotation) as GameObject;
        iswallUp = Physics2D.Raycast(transform.position, Vector2.up, 1.0f, wallmask);
        iswallDown = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, wallmask);
        iswallLeft = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, wallmask);
        iswallRight = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, wallmask);
        //爆炸长度
        if (!iswallUp)
        {
            for (int i = 0; i < bombDistance; i++)
            {
                ItemfireU = Instantiate(Resources.Load("Prefabs/fire"), new Vector3(
                transform.position.x, transform.position.y + i, transform.position.z), transform.rotation) as GameObject;
            }
        }
        if (!iswallDown)
        {
            for (int i = 0; i < bombDistance; i++)
            {
                ItemfireD = Instantiate(Resources.Load("Prefabs/fire"), new Vector3(
                transform.position.x, transform.position.y - i, transform.position.z), transform.rotation) as GameObject;
            }
        }
        if (!iswallLeft)
        {
            for (int i = 0; i < bombDistance; i++)
            {
                ItemfireL = Instantiate(Resources.Load("Prefabs/fire"), new Vector3(
                transform.position.x - i, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            }
        }
        if (!iswallRight)
        {
            for (int i = 0; i < bombDistance; i++)
            {
                ItemfireU = Instantiate(Resources.Load("Prefabs/fire"), new Vector3(
                transform.position.x + i, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            }
        }
    }
}
