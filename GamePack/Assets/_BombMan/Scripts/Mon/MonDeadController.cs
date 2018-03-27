using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonDeadController : MonoBehaviour
{
    public AudioSource sound;
    void Start()
    {
        sound.clip = (AudioClip)Resources.Load("Music/DefaultMusic/Audio/Dead_" + Random.Range(1, 5), typeof(AudioClip));
        sound.Play();
        StartCoroutine(MonStateTime(1.5f));
    }

    IEnumerator MonStateTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
