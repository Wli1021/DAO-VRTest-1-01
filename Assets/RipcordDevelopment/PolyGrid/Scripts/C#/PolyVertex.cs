using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 						    PolyGrid 1.0, Copyright © 2018, Ripcord Development
//											  PolyVertex.cs
//										   info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - This script controls the movement of each individual polyGrid vertex.

namespace PolyGrid
{
    public class PolyVertex : MonoBehaviour
    {
        [HideInInspector]	public float vertexRange;		//The maximum amount the vertex can move from the origin on any one axis
        Vector3 originalPosition;							//The local position of the vertex is at when the script starts

        [HideInInspector]	public float minTime;			//The minimum amount of seconds for the vertex to move to the next position
        [HideInInspector]	public float maxTime;			//The maximum amount of seconds for the vertex to move to the next position

        [HideInInspector]	public PolyGrid polyGrid;		//A reference to the polyGrid object the vertex belongs to


        void Start () {
        
            originalPosition = gameObject.transform.localPosition;		                                                                //Save the original local position of the vertex

            if (polyGrid.autoStart)                                                                                                     //If the polyGrid is set to start automatically...
            {
                MoveVertex();											                                                                //...start moving the vertex
            }
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // MOVE VERTEX - Selects a random destination for the vertex then moves it to that location over a random amount of time
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void MoveVertex ()
        {
            float randomX = Random.Range(originalPosition.x - vertexRange, originalPosition.x + vertexRange);	                        //Select a random position on the x axis within the vertex movement range
            float randomY = Random.Range(originalPosition.y - vertexRange, originalPosition.y + vertexRange);	                        //Select a random position on the y axis within the vertex movement range
            float randomZ = Random.Range(originalPosition.z - vertexRange, originalPosition.z + vertexRange);	                        //Select a random position on the z axis within the vertex movement range

            Vector3 newPosition = new Vector3(randomX, randomY,randomZ);										                        //Create a new vector 3 using the 3 random positions that were generated

            float randomTime = Random.Range(minTime, maxTime);													                        //Select a random movement time from the min and max time values

            iTween.MoveTo(gameObject, iTween.Hash(
                "Position", newPosition, "Time", randomTime / polyGrid.speedMultiplier, 
                "IsLocal", true, "EaseType", iTween.EaseType.easeInOutQuad,
                "OnComplete", "MoveVertex", "OnCompleteTarget", gameObject
            ));                                                                                                                         //Move the vertex to the new position over the random time.  When finished, repeat this function
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // RESET VERTEX POSITION - Move the vertex back to its original position over a random amount of time
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void ResetVertex ()
        {
            float randomTime = Random.Range(minTime, maxTime);													                        //Select a random movement time from the min and max time values

            iTween.MoveTo(gameObject, iTween.Hash(
                "Position", originalPosition, "Time", randomTime / polyGrid.speedMultiplier, 
                "IsLocal", true, "EaseType", iTween.EaseType.easeInOutQuad
            ));                                                                                                                         //Move the vertex back to its original position over the random time
        }
    }
}
