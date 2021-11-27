using System;
using System.Collections.Generic;
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
    private Transform _firePoint ;
    // private bool displayRadius = false;
    
    [NonSerialized] public List<TowerEnemy> Targets = new List<TowerEnemy>();

    void Start()
    {
        _firePoint = transform;
        _passedTime = attackSpeed + 1;
    }
    void FixedUpdate()
    {
        if (Targets.Count > 0)
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

    private TowerEnemy GetCurrentTarget()
    {
        if (Targets.Count == 0)
        {
            return null;
        }

        TowerEnemy target = Targets[0];
        for (int i = 1; i < Targets.Count; i++)
        {
            if (Targets[i].DistanceTraveled > target.DistanceTraveled)
            {
                target = Targets[i];
            }
        }
        
        return target;
    }

    private void Fire(Vector3 targetPos)
    {
        Vector3 projectileFirePoint = _firePoint.position;
        projectileFirePoint.z = -4;
        GameObject newObj = Instantiate(projectilePrefab, projectileFirePoint, _firePoint.rotation);
        Bullet projectile = newObj.GetComponent<Bullet>();
        projectile.bulletForce = projectileSpeed;
        projectile.damage = projectileDamage;

        Vector2 direction = targetPos - projectileFirePoint;
        direction.Normalize();
        projectile.direction = direction;
    }
}
