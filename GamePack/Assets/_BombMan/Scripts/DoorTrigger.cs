using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum doorMonState
{
    defaultMon,
    openMon,
    closeMon
}
public class DoorTrigger : MonoBehaviour
{
    public doorMonState doorMon;
    private void Start()
    {
        doorMon = doorMonState.defaultMon;
        StartCoroutine(DoorTriggerFire(0.6f));
    }
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //炸门产生6个
        if (collision.tag == "Boom"&&doorMonState.openMon==doorMon)
        {
            for (int m = 1; m <= 7; m++)
            {
                GameObject go = Instantiate(Resources.Load("Prefabs/Mon" + m), transform.position, Quaternion.identity) as GameObject;
                GameContorller._instance.enemyCount++;
                UIController.enemyCount = GameContorller._instance.enemyCount;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UIController>().StartViewWithIndex(5);
            }
        }
    }
    IEnumerator DoorTriggerFire(float time)
    {
        yield return new WaitForSeconds(time);
        doorMon = doorMonState.openMon;

    }
}
