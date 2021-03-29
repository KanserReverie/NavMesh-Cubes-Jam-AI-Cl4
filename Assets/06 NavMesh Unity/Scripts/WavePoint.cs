using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePoint : MonoBehaviour
{
    // Lambda - Points to another variable or function
    // In this case, it makes Position = transfor.position
    public Vector3 Position => transform.position;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Position,0.5f);
    }


}
