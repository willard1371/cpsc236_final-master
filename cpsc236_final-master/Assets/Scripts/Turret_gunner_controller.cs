using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_gunner_controller : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Instantiate(bullet, transform);
        }

    }
}
