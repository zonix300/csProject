using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 1.5f;

    private Vector3 position;

    void Start()
    {
        if(!player)
            player = FindObjectOfType<Hero>().transform;
        
        
    }


    void Update()
    {
        position = player.position;
        position.z = -10f;

        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    
    }
}
