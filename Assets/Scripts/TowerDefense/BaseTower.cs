using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    public float attackSpeed;
    public float projectileSpeed;
    public float projectileDamage;
    public float radiusScale;
    public float cost;
    private float _passedTime = 0f;
    private Transform firePoint ;
    private bool displayRadius = false;
    
    [NonSerialized] public bool isActivated = false;
    [NonSerialized] public List<TowerEnemy> targets = new List<TowerEnemy>();

    void Start()
    {
        firePoint = transform;
        _passedTime = attackSpeed + 1;
    }
    void FixedUpdate()
    {
        if (targets.Count > 0)
        {
            TowerEnemy target = GetCurrentTarget();
            _passedTime += Time.deltaTime;
            if (_passedTime > attackSpeed)
            {
                _passedTime = 0;
                Fire(target.transform.position);
            }
        }
    }

    public TowerEnemy GetCurrentTarget()
    {
        if (targets.Count == 0)
        {
            return null;
        }

        TowerEnemy target = targets[0];
        for (int i = 1; i < targets.Count; i++)
        {
            if (targets[i].distanceTraveled > target.distanceTraveled)
            {
                target = targets[i];
            }
        }
        
        return target;
    }

    private void Fire(Vector3 targetPos)
    {
        GameObject newObj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Bullet projectile = newObj.GetComponent<Bullet>();
        projectile.bulletForce = projectileSpeed;
        projectile.damage = projectileDamage;

        Vector2 direction = targetPos - firePoint.position;
        direction.Normalize();
        projectile.direction = direction;
    }
}
