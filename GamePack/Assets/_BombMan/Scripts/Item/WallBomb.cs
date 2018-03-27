using UnityEngine;
using System.Collections;

public class WallBomb : MonoBehaviour {
    private int firef;
    bool iswallUp = false;
    // Use this for initialization
    void Start () {
        firef = LayerMask.GetMask("Fire");
    }
	
	// Update is called once per frame
	void Update () {

        iswallUp = Physics2D.Raycast(transform.position, Vector2.zero, 0.0f, firef);
        if (iswallUp)
        {
            Destroy(gameObject);
        }
    } 
}
