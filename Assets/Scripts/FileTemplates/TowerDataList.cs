using UnityEngine;

[System.Serializable]
public class TowerDataList
{
    public TowerData[] towerList;
    private int len;
    private int cur;

    public TowerDataList(int size)
    {
        towerList = new TowerData[size];
        len = size;
        cur = 0;
    }

    public void AddTower(BaseTower tower)
    {
        if (cur >= len)
        {
            return;
        }
        
        TowerData data = new TowerData(tower.transform.position, tower.GetTier(), tower.towerType);
        towerList[cur++] = data;
    }
    
    public void AddTower(TowerData tower)
    {
        if (cur >= len)
        {
            return;
        }
        
        TowerData data = new TowerData(
            new Vector3(tower.location[0], tower.location[1], tower.location[2]),
            0, tower.type);
        towerList[cur++] = data;
    }
    
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}
