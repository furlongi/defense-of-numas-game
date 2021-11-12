using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Camera cam;

    public float bulletForce = 10f;
    public int upgradeTier = 0;

    private void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(cam.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void Shoot(Vector3 mousePos)
    {
        GameObject newObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = newObj.GetComponent<Bullet>();

        Vector2 direction = mousePos - firePoint.position;
        direction.Normalize();
        
        bullet.direction = direction;
    }
}
