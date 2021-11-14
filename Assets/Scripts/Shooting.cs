using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform pivot;
    public GameObject bulletPrefab;
    public Transform bulletPoint;
    public Camera cam; // Assign with inspector
    


    public float[] bulletDamage = new float[]{1f, 3f, 5f, 8f};
    public int upgradeTier = 0;

    private SpriteRenderer _sprite;

    // Assign with inspector
    public Sprite GreenLaser;
    public Sprite BlueLaser;
    public Sprite RedLaser;
    public Sprite PurpleLaser;


    private void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        _sprite = GetComponent<SpriteRenderer>();
        transform.parent = pivot;

    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Vector3 lookDir = cam.WorldToScreenPoint(pivot.transform.position);
        lookDir = Input.mousePosition - lookDir;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        pivot.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        var zPos = pivot.transform.localRotation.eulerAngles.z;

        if (zPos > 90 && zPos < 270)
        {
            _sprite.flipY = true;
        }
        else
        {
            _sprite.flipY = false;
        }

        // var zPoss = zPos * Mathf.Rad2Deg;
        // Debug.Log("L " + bulletPoint.position.x + " " + bulletPoint.position.y);
        // Debug.Log("R " + Math.Cos(zPoss) * bulletPoint.position.x + " " + Math.Sin(zPoss) * bulletPoint.position.y);
    }

    public void Shoot()
    {
        Debug.Log(transform.right);
        GameObject newObj = Instantiate(bulletPrefab, bulletPoint.position, transform.rotation);
        Bullet bullet = newObj.GetComponent<Bullet>();
        SpriteRenderer bulletSprite = bullet.GetComponent<SpriteRenderer>();

        switch (upgradeTier)
        {
            case 0:
                bulletSprite.sprite = GreenLaser;
                break;
            case 1:
                bulletSprite.sprite = BlueLaser;
                break;
            case 2:
                bulletSprite.sprite = RedLaser;
                break;
            case 3:
                bulletSprite.sprite = PurpleLaser;
                break;
            default:
                bulletSprite.sprite = GreenLaser;
                break;
        }
        bullet.damage = bulletDamage[upgradeTier];

        Vector2 direction =  bulletPoint.position - transform.position;
        direction.Normalize();
        
        bullet.direction = direction;
    }
}

public static class WeaponInfo
{

}
