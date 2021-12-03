
using UnityEngine;

[System.Serializable]
public class TowerData
{
    public float[] location;
    public int tier;
    public int type;

    public TowerData(Vector3 loc, int upTier, TowerType ttype)
    {
        location = new float[3];
        location[0] = loc.x;
        location[1] = loc.y;
        location[2] = loc.z;
        tier = upTier;
        type = (int)ttype;
    }
    
    public TowerData(Vector3 loc, int upTier, int ttype)
    {
        location = new float[3];
        location[0] = loc.x;
        location[1] = loc.y;
        location[2] = loc.z;
        tier = upTier;
        type = ttype;
    }

}
