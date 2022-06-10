using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;


        private void FixedUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
