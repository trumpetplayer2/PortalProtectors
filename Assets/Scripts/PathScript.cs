using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public PathScript next;

    private void OnDrawGizmos()
    {
        if (next == null) { return; }
        Debug.DrawLine(next.transform.position, this.transform.position, Color.red);
    }
}
