using System;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;


public class BaseTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public TowerType towerType;

    public Sprite tierZeroTower;
    public Sprite tierOneTower;
    public Sprite tierTwoTower;
    public Sprite tierThreeTower;

    public Sprite greenLaser;
    public Sprite blueLaser;
    public Sprite purpleLaser;
    public Sprite redLaser;
    
    public float attackSpeed;
    public float projectileSpeed;
    
    private float _passedTime = 0f;
    private Transform _firePoint ;
    private float _damage;
    
    [NonSerialized] public List<TowerEnemy> Targets = new List<TowerEnemy>();

    private int _upgradeTier = 0;
    private EventManager _eventManager;

    void Start()
    {
        Renderer radiusRenderer = transform.GetChild(0).GetComponent<Renderer>();
        _eventManager = GameObject.Find("Tower Event Manager").GetComponent<EventManager>();
        _eventManager.TowerShop.SetActive(false);

        _eventManager.TowerUpgradeShop.SetTower(this);
        _eventManager.TowerUpgradeShop.gameObject.SetActive(true);
        
        _eventManager.TowerRadiuses.Add(radiusRenderer);
        
        _firePoint = transform;
        _passedTime = (1 / attackSpeed) + 1;
        CircleCollider2D col = GetComponentInChildren<TowerRadius>().gameObject.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
        RefreshTierData();
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
        SpriteRenderer spriteR = newObj.GetComponent<SpriteRenderer>();
        if (_upgradeTier == 0)
        {
            spriteR.sprite = greenLaser;
        }
        else if (_upgradeTier == 1)
        {
            spriteR.sprite = blueLaser;
        }
        else if (_upgradeTier == 2)
        {
            spriteR.sprite = purpleLaser;
        }
        else if (_upgradeTier == 3)
        {
            spriteR.sprite = redLaser;
        }
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
        projectile.damage = _damage;
        projectile.direction = direction;
    }

    public int GetTier()
    {
        return _upgradeTier;
    }
    
    public void UpgradeTower()
    {
        if (_upgradeTier < TowerCostData.MAXTier)
        {
            _upgradeTier++;
            RefreshTierData();
        }
        else
        {
            Debug.Log("Already at max tier.");
        }
    }

    public void RefreshTierData()
    {
        if (_upgradeTier == 0)
        {
            GetComponent<SpriteRenderer>().sprite = tierZeroTower;
            projectilePrefab.GetComponent<SpriteRenderer>().sprite = greenLaser;
        }
        if (_upgradeTier == 1)
        {
            GetComponent<SpriteRenderer>().sprite = tierOneTower;
        }
        else if (_upgradeTier == 2)
        {
            GetComponent<SpriteRenderer>().sprite = tierTwoTower;
        }
        else if (_upgradeTier == 3)
        {
            GetComponent<SpriteRenderer>().sprite = tierThreeTower;
        }

        if (towerType == TowerType.Heavy)
        {
            _damage = TowerTierDamageData.HeavyTower[_upgradeTier];
        }
        else if (towerType == TowerType.Normal)
        {
            _damage = TowerTierDamageData.MediumTower[_upgradeTier];
        }
        else if (towerType == TowerType.Sniper)
        {
            _damage = TowerTierDamageData.LightTower[_upgradeTier];
        }
    }

    // SHOULD ONLY BE USABLE BY EVENTMANAGER WHEN SPAWNING SAVED TOWERS
    public void SetTier(int tier)
    {
        if (tier <= TowerCostData.MAXTier)
        {
            _upgradeTier = tier;
            RefreshTierData();
        }
    }
    
}
