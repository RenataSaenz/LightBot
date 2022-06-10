using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Vehicles.Ball;

public class Platform : MonoBehaviour
{
    
    [SerializeField] List <Transform> waypoints;
    [SerializeField] float moveSpeed;
    [SerializeField] private float _timeBeforeMoving = 3;
   // [SerializeField] private float _travelTime;
    bool _stop = false;
    private int waypointIndex = 0;
   // private Vector3 currentPos;
    
    private void FixedUpdate()
    {
            Move();
    }

    private void Move()
    {
        if (!_stop)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position,
                 moveSpeed * Time.deltaTime);
            
            // currentPos = Vector3.Lerp(waypoints[0].transform.position,
            //     waypoints[1].transform.position,
            //     Mathf.Cos(Time.time / _travelTime * Mathf.PI * 2)* -.5F + .5F);
            // _rb.MovePosition(currentPos);

            if (Vector3.Distance(waypoints[waypointIndex].transform.position, transform.position) < 0.1f)
            {
                waypointIndex++;
                if (waypointIndex > waypoints.Count - 1)
                {
                    waypointIndex = 0;
                    waypoints.Reverse();
                    StartCoroutine(MoveAfterTime());
                }
            }
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     cc = other.gameObject.GetComponent<CharacterController>();
    // }
    // private void OnTriggerStay(Collider other)
    // {
    //     cc.Move(_rb.velocity*Time.deltaTime);
    // }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.ToString());
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log(this.gameObject.ToString() + " detected col");
            player.transform.parent = gameObject.transform;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {  
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log(this.gameObject.ToString() + " EXIT detected col");
            player.transform.parent = null;
        }
        
    }
    
    private IEnumerator MoveAfterTime()
    {
        _stop = true;
        yield return new WaitForSeconds(_timeBeforeMoving);
        _stop =false;
    } 
}
