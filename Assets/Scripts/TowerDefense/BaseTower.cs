using System;
using System.Collections.Generic;
using UnityEngine;


public class BaseTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public TowerType towerType;
    
    public float attackSpeed;
    public float projectileSpeed;
    public float projectileDamage;
    
    private float _passedTime = 0f;
    private Transform _firePoint ;

    [NonSerialized] public List<TowerEnemy> Targets = new List<TowerEnemy>();

    private int _upgradeTier = 0;
    private EventManager _eventManager;

    void Start()
    {
        Renderer radiusRenderer = transform.GetChild(0).GetComponent<Renderer>();
        _eventManager = GameObject.Find("Tower Event Manager").GetComponent<EventManager>();
        _eventManager.TowerShop.SetActive(false);

        _eventManager.TowerUpgradeShop.gameObject.SetActive(true);
        _eventManager.TowerUpgradeShop.SetTower(this);
        
        _eventManager.TowerRadiuses.Add(radiusRenderer);
        
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
        projectileFirePoint.z -= 1;
        Vector3 direction = targetPos - projectileFirePoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotationAngle = Quaternion.AngleAxis (angle, Vector3.forward);
        GameObject newObj = Instantiate(projectilePrefab, projectileFirePoint, rotationAngle);

        if (towerType == TowerType.Normal)
        {
            newObj.transform.localScale *= 2f;
        }
        else if (towerType == TowerType.Sniper)
        {
            newObj.transform.localScale *= 4f;
        }
        direction.Normalize();
        Bullet projectile = newObj.GetComponent<Bullet>();

        projectile.bulletForce = projectileSpeed;
        projectile.damage = projectileDamage;
        projectile.direction = direction;
    }

    public int GetTier()
    {
        return _upgradeTier;
    }
    
    public void UpgradeTower()
    {
        _upgradeTier++;
    }
}
