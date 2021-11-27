using UnityEngine;

public class TowerRadius : MonoBehaviour
{
    private Transform _objectTransform;
    void Start()
    {
        _objectTransform = gameObject.transform;
        var radiusScale = gameObject.GetComponentInParent<BaseTower>().radiusScale;

        _objectTransform.localScale = new Vector3(10 * radiusScale, 18 * radiusScale, 1);
        _objectTransform.position = _objectTransform.parent.position;
    }

    void Update()
    {
        _objectTransform.position = _objectTransform.parent.position;
    }
}
