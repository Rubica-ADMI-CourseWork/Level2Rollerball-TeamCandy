using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtility : MonoBehaviour
{
   public Vector3 GetForwardVector()
    {
        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
        return rot * Vector3.forward;
    }

    public Vector3 GetRightVector()
    {
        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
        return rot * Vector3.right;
    }
}
