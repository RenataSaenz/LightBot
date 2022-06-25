using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyStop : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;
        GameManager.instance.ResetPosition();
    }
}
