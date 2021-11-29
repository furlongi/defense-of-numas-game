using System;
using System.Collections.Generic;
using UnityEngine;


public class BaseTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    public float attackSpeed;
    public float projectileSpeed;

    private float _passedTime = 0f;
    private Transform _firePoint ;

    [NonSerialized] public CancelPopup CancelPopup;
    [NonSerialized] public List<TowerEnemy> Targets = new List<TowerEnemy>();

    void Start()
    {
        Renderer radiusRenderer = transform.GetChild(0).GetComponent<Renderer>();
        CancelPopup = GameObject.Find("Popup Closer").GetComponent<CancelPopup>();
        CancelPopup.TowerRadiuses.Add(radiusRenderer);
        
        _firePoint = transform;
        _passedTime = (1 / attackSpeed) + 1;
        CircleCollider2D col = GetComponentInChildren<TowerRadius>().gameObject.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
    }
    void FixedUpdate()
    {
        if (Targets.Count > 0)
        {
            TowerEnemy target = GetCurrentTarget();
            _passedTime += Time.deltaTime;
            if (_passedTime > (1 / attackSpeed))
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
        //projectile.damage = projectileDamage;

        Vector2 direction = targetPos - projectileFirePoint;
        direction.Normalize();
        projectile.direction = direction;
    }
}
