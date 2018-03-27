using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    public AudioSource sound;
    // Use this for initialization
    void Start () {
        StartCoroutine(fire());
        sound.clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/Bomb" + Random.Range(0, 5), typeof(AudioClip));
        sound.Play();
    }
    
    
    // Update is called once per frame
    void Update () {
	
	} 
    //爆炸触发刚体
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.collider.tag == "WallBomb")
        { 
            Destroy(collision.collider.gameObject);
        }
        if(collision.collider.tag == "Player")
        {
            //扩展
        }
        if(collision.collider.tag=="Bomb")
        {
            Debug.Log("接触到炸弹");
        }

    }
    //爆炸触发物体
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WallBomb")
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Player")
        {
            Destroy(collision.gameObject);
            Instantiate(Resources.Load("Prefabs/playerDead"), collision.transform.position, Quaternion.identity);
            GameContorller._instance.gameOver();

        }
        if (collision.tag == "Bomb")
        {
            //Debug.Log("接触到炸弹");
        }
        if (collision.tag == "DoorWall")
        {
            Instantiate(Resources.Load("Prefabs/Door"), collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        //怪物触发死亡
        for (int n = 0; n < 7; n++)
        {
            if (collision.tag == "Mon" + n&&EnemyAI._instance.monFireState == monFire.monFireOpen)
            {
                Destroy(collision.gameObject);
                GameObject go = Instantiate(Resources.Load("Prefabs/MonDead/Mon" + n), collision.transform.position, Quaternion.identity) as GameObject;
            }
        }

    }
    IEnumerator fire()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    } 

}
