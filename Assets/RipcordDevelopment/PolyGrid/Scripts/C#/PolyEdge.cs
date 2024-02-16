using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 						    PolyGrid 1.0, Copyright © 2018, Ripcord Development
//											   PolyEdge.cs
//										   info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - This script links the points of the edge object's LineRenderer to the individual vertices in the PolyGrid

namespace PolyGrid
{
    public class PolyEdge : MonoBehaviour
    {
        LineRenderer lineRenderer;											//A reference to the line renderer on this object
        //[HideInInspector]
        public List<Transform> vertices;				//A list of all the vertices the line renderer will pass through

        void Start ()
        {
            lineRenderer = this.GetComponent<LineRenderer>();				//Create a reference to the line renderer on this object
        }

        void Update ()
        {
            for (int x = 0; x < vertices.Count; x++)                        //Cycle through each of the vertices in the list...
            {
                lineRenderer.SetPosition(x, vertices[x].localPosition);		//...set the lineRenderer position to match the local position of the corresponding vertex
            }
        }
    }
}
