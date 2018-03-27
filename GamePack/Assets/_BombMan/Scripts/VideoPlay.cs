using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoPlay : MonoBehaviour {
    public MovieTexture movie;
    // Use this for initialization
    void Start () {
        movie.Play();
    } 
}
