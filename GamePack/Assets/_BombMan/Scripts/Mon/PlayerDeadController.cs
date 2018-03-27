using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadController : MonoBehaviour
{
    public AudioSource sound;
    void Start()
    {
        sound.clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/playerDead" + Random.Range(1,2), typeof(AudioClip));
        sound.Play();
        StartCoroutine(MonStateTime(1.5f));
    }

    IEnumerator MonStateTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
