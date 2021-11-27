using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private Vector2 _position;
    
    public Vector2 GetPosition()
    {
        return this._position;
    }
    
    void Start()
    {
        Vector3 pos = transform.position;
        gameObject.transform.position = new Vector3(pos.x, pos.y, -100);
        _position = (Vector2) pos;
    }
}
