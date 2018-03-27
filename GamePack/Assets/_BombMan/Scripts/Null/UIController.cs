using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// 根据原作者Foldcc基础，进行修改添加
/// </summary>
public class UIController : MonoBehaviour
{
    public GameObject[] UIview;

    public AudioClip startBackgroundAudio;

    public AudioClip backgroundAudio;

    public AudioClip loadAudio;

    public AudioClip lossAudio;

    public AudioClip BTG_Audio;

    public AudioClip doorFireAudio;

    public static int gameLevel = 0;

    public static int enemyCount = 0; 

    public static int gameTime = 100;

    public Text levelText;
    public Text enemyCountText;
    public Text playerHPText;
    public Text gameTimeText;
    public void StartViewWithIndex(int index)
    {
        for (int i = 0; i < UIview.Length; i++)
        {
            UIview[i].SetActive(false);
        }
        UIview[index].SetActive(true);
        if (index == 0)
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = startBackgroundAudio;

        }
        else if (index == 1)
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = backgroundAudio;

        }
        else if (index == 2)
        {
            GetComponent<AudioSource>().clip = loadAudio;
        }
        else if (index == 3)
        {
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().clip = lossAudio;
        }
        else if (index == 4)
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = BTG_Audio;
        }
        else if (index == 5)
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = doorFireAudio;
        }
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

    }

    void OnGUI()
    {
        levelText.text = "关卡: " + gameLevel;
        enemyCountText.text = "怪物数量: " + enemyCount; 
        gameTimeText.text = "剩余时间: " + gameTime;
        if (gameTime <= 20)
        {
            GameContorller._instance.musicSwith = musicController.bebeOpen;
            gameTimeText.color = Color.red;
        }
        else
        {
            gameTimeText.color = Color.white;
        }
    }

}
