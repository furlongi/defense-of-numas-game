using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Anchors are bounds to where the camera can move.
 * For example, if the upper Y limit is 2.0, then the
 * camera will not move beyond 2.0 while following the player, but
 * the player can still move beyond that.
 *
 * This is useful for when you do not want to show an empty area in a map
 * (such as the hub world) and limit the camera to look in restricted
 * areas only.
 *
 * The locks are to stop transformations on an axis.
 * ->isLocked: Stops the camera from moving
 * ->isXLocked: Stops horizontal movement
 * ->isYLocked: Stops vertical movement
 *
 * -- How To Find Anchor Points: --
 *  - This will use upper Y limit (upAnchorY) as the example
 * 
 * 1. Run the game in Unity
 * 2. Walk upwards until the empty area from the map is shown
 *    (the blue background area if using Unity defaults)
 * 3. Using the inspector, go to the Camera object in the
 *    Hierarchy and find this script
 * 4. Slide the values, so that the camera starts to go downwards,
 *    until the background is no longer seen
 */

public class FollowCharacter : MonoBehaviour
{
    // Assign to player or object in inspector
    public GameObject target;
    
    // Anchor bounds. Assign in inspector. If zero, defaults to +-infinite.
    public float upAnchorY;
    public float lowAnchorY;
    public float rightAnchorX;
    public float leftAnchorX;
    
    // Locks
    public bool isLocked;
    public bool isXLocked;
    public bool isYLocked;
    
    // Z Camera offset to view objects correctly
    private float _offsetZ;
    
    
    void Start()
    {
        _offsetZ = transform.position.z - target.transform.position.z;

        if (upAnchorY == 0)
        {
            upAnchorY = float.MaxValue;
        }
        if (lowAnchorY == 0)
        {
            lowAnchorY = float.MinValue;
        }
        if (rightAnchorX == 0)
        {
            rightAnchorX = float.MaxValue;
        }
        if (leftAnchorX == 0)
        {
            leftAnchorX = float.MinValue;
        }
    }


    private void LateUpdate()
    {
        if (isLocked)
        {
            return;
        }

        float newY = transform.position.y;
        float newX = transform.position.x;
        float targetY = target.transform.position.y;
        float targetX = target.transform.position.x;

        if (!isYLocked)
        {
            if (targetY < lowAnchorY)
            {
                newY = lowAnchorY;
            }
            else if (targetY > upAnchorY)
            {
                newY = upAnchorY;
            }
            else
            {
                newY = targetY;
            }
        }

        if (!isXLocked)
        {
            if (targetX < leftAnchorX)
            {
                newX = leftAnchorX;
            } 
            else if (targetX > rightAnchorX)
            {
                newX = rightAnchorX;
            }
            else
            {
                newX = targetX;
            }
        }

        transform.position = new Vector3(newX, newY, target.transform.position.z + _offsetZ);
    }
}
