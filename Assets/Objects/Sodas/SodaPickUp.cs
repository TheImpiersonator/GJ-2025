using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaPickUp : MonoBehaviour
{
    [SerializeField] Rigidbody body;

    private void Start()
    {
        
    }

    private void Update()
    {
        Spin();
    }


}
