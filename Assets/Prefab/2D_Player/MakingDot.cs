using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakingDot : MonoBehaviour {

    public Color lineColor = Color.magenta;
    public List<Transform> actualPath = new List<Transform>();
    Transform[] pathArray;

    void Start()
    {
        actualPath.Add(this.transform);
    }
  
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        pathArray = this.GetComponentsInChildren<Transform>();
        actualPath.Clear();
        int name = 0;
        foreach (Transform element in pathArray)
        {
            if (element != this.transform)
            {
                element.name = name.ToString();
                name++;
                actualPath.Add(element);
            }
        }

        for (int i = 0; i < actualPath.Count; i++)
        {
            Vector3 currentPostion = actualPath[i].position;
            if (i > 0)
            {
                Vector3 previousPostion = actualPath[i - 1].position;
                Gizmos.DrawLine(previousPostion, currentPostion);   // allow the use of drawing lines between any two nodes 
                Gizmos.DrawWireSphere(currentPostion, 0.5f);
            }
            else
            {
                Gizmos.DrawWireSphere(currentPostion, 0.5f);
            }
        }
    }
}

