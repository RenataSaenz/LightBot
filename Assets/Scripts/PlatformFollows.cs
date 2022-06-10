using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFollows : MonoBehaviour
{
    //private GameObject platformStep;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //platformStep = GameObject.FindGameObjectWithTag("PlatformChild");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.parent = this.gameObject.transform;
        }
    }

    // private void OnCollisionStay(Collision collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         player.transform.parent = this.gameObject.transform;
    //     }
    // }
    //
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.parent = null;
        }
    }
}
