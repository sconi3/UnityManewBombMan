using UnityEngine;
using System.Collections;
//主角按键状态机
public enum playerState
{
    _KEYOPEN,
    _KEYCLOSE
}
//主角定时炸弹状态机
public enum bombState
{
    openBomb,
    closeBomb,
    timeBomb
}
//主角穿越墙体状态机
public enum playerTransparent
{
    defaultTriggerOn,
    wallTriggerOn,
    bombTriggerOn,
    boomTriggerOn,
    monTriggerOn
}



public class Player : MonoBehaviour
{
    public GameObject bombType;
    GameObject bomb;
    /// <summary>
    /// 主角速度
    /// </summary>
    public float speed = 2.0f;

    /// <summary>
    /// 炸弹数量单例模式
    /// </summary>
    public static Player _instance;
    //炸弹消耗量
    public int bombNum = 1;

    /// <summary>
    /// 炸弹距离控制
    /// </summary>
    public int bombHeight = 2;

    /// <summary>
    /// 炸弹状态控制
    /// </summary>
    public playerState _KeyPress;

    /// <summary>
    /// 定时炸弹控制
    /// </summary>
    public bombState bombTime;  //炸弹状态0OPEN 1CLOSE
    public bombState bombController; //定时炸弹状态

    /// <summary>
    /// 人物状态控制
    /// </summary>
    public playerTransparent playerT;

    // Use this for initialization
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().MovePosition(transform.position + new Vector3(h, v, 0) * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Animator>().SetBool("ddown", true);
            //StartCoroutine(playerMoveAudio(0.3f));
             
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            GetComponent<Animator>().SetBool("ddown", false);
            GetComponent<AudioSource>().loop = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Animator>().SetBool("uup", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            GetComponent<Animator>().SetBool("uup", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("lleft", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("lleft", false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("rright", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("rright", false);
        }

        //放置炸弹的数量控制，原型CODE
        if (Input.GetKeyDown(KeyCode.K))
        {
            if(bombNum<=0)
            {
                GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/PressFcBomb", typeof(AudioClip));
                GetComponent<AudioSource>().Play();
            }
            if (playerState._KEYOPEN == _KeyPress)
            {
                if (bombNum == 0)
                {
                    //扩展
                }
                if (bombNum == 1)
                {
                    bomb = Instantiate(Resources.Load("Prefabs/bomb"), new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)), transform.rotation) as GameObject;

                }
                if (bombNum == 2)
                {
                    bomb = Instantiate(Resources.Load("Prefabs/bomb"), new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)), transform.rotation) as GameObject;

                }
                if (bombNum == 3)
                {
                    bomb = Instantiate(Resources.Load("Prefabs/bomb"), new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)), transform.rotation) as GameObject;

                }
                if (bombNum == 4)
                {
                    bomb = Instantiate(Resources.Load("Prefabs/bomb"), new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)), transform.rotation) as GameObject;

                }
                if (bombNum == 5)
                {
                    bomb = Instantiate(Resources.Load("Prefabs/bomb"), new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)), transform.rotation) as GameObject;

                }
            }
        }
        //按键J，控制定时炸弹引爆
        if(Input.GetKeyDown(KeyCode.J))
        {
            if (bombController == bombState.timeBomb)
            {
                bombTime = bombState.openBomb;
            } 
        } 
    }
    //触发了穿过墙体、炸弹、火焰的方法
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.transform.tag == "WallBomb")
        {
            if (playerTransparent.wallTriggerOn == playerT)
            {
                collision.transform.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
        if(collision.transform.tag=="Mon")
        {
            if (playerTransparent.monTriggerOn == playerT)
            {
                collision.transform.GetComponent<BoxCollider2D>().isTrigger = true;
                playerT = playerTransparent.wallTriggerOn;
                playerT = playerTransparent.bombTriggerOn;
            }
        }
    }
    //接触到Trigger物体
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //是否触碰到炸弹，判断在炸弹上不能放置炸弹
        if (collision.tag == "Bomb")
        {
            _KeyPress = playerState._KEYCLOSE;
        }
        //触发到道具
        if (collision.tag == "Props")
        {
            GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/ItemUp", typeof(AudioClip));
            GetComponent<AudioSource>().Play();
        }
        //主角移动速度道具
        if(collision.tag== "UpBombSpeed")
        {
            speed=5;
            Destroy(collision.gameObject);
        }
        //触发到增加炸弹数量道具
        if (collision.tag == "UpBombNum")
        {
            bombNum++;
            Destroy(collision.gameObject);
        }
        //触发到增加爆炸距离的道具
        if (collision.tag == "UpBombFire")
        {
            bombHeight++;
            Destroy(collision.gameObject);
        }
        //触发到定时炸弹
        if (collision.tag == "UpBombTime")
        { 
            bombTime = bombState.closeBomb;
            bombController = bombState.timeBomb;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Boom")
        {
            if (playerTransparent.boomTriggerOn == playerT)
            {
                //火焰炸到角色的扩展
                transform.GetComponent<BoxCollider2D>().isTrigger = true; 
            }
        }
        if (collision.gameObject.CompareTag("Door"))
        {
            if (UIController.enemyCount <= 0)
            {
                //游戏通关
                StartCoroutine(BeatTheGame(1f));
                collision.gameObject.GetComponent<AudioSource>().enabled = true;

            }
        }
    } 
    //离开了Trigger物体
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bomb"&& playerTransparent.bombTriggerOn==playerT)
        {
            _KeyPress = playerState._KEYOPEN;
            collision.transform.GetComponent<BoxCollider2D>().isTrigger = true;
            return;
        }
        else
        {
            _KeyPress = playerState._KEYOPEN;
            //collision.transform.GetComponent<BoxCollider2D>().isTrigger = false;
             
        }
    }
    public void playBeatTheGameAudio()
    {
        GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/LevelUp", typeof(AudioClip));
        GetComponent<AudioSource>().Play();
    }
    IEnumerator BeatTheGame(float time)
    {
        yield return new WaitForSeconds(time);
        //通全关
        if(UIController.gameLevel==10)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UIController>().StartViewWithIndex(4);
        }
        else
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameContorller>().StartGame();
        }

    }
    //脚步声音
    //IEnumerator playerMoveAudio(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/playerRight", typeof(AudioClip));
    //    GetComponent<AudioSource>().Play();
    //    yield return new WaitForSeconds(0.3f);
    //    GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/playerLeft", typeof(AudioClip));
    //    GetComponent<AudioSource>().Play();
    //}
}
