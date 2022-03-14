using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LernProject
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform _rotatePoint;
        

        private void OnTriggerEnter(Collider other)

        {
            if (other.CompareTag("Player") )
            {
                _rotatePoint.rotation = Quaternion.LookRotation(transform.position + Vector3.left);
            }
        }
                     
    }
}
