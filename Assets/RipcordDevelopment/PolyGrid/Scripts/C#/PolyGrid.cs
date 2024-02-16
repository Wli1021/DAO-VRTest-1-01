using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 						    PolyGrid 1.0, Copyright © 2018, Ripcord Development
//											   PolyGrid.cs
//										   info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - Creates a dynamic grid of vertex and edge objects, as well as sets the visual style and motion for the grid

namespace PolyGrid
{
    [AddComponentMenu("RipcordDev/PolyGrid/PolyGrid")]
    public class PolyGrid : MonoBehaviour
    {
        //GRID CONSTRUCTION
        [HideInInspector]	public GameObject vertex;					//The gameObject that will be used for all vertex objects
        [HideInInspector]	public Material vertexMaterial;				//The material that will be applied to all vertex objects
        [HideInInspector]	public Material edgeMaterial;				//The material that will be applied to all edge objects

        //GRID DIMENSIONS
        [HideInInspector]	public int rows = 5;						//The number of vertex rows the polyGrid object will have
        [HideInInspector]	public int columns = 5;						//The number of vertex columns the polyGrid object will have
        [HideInInspector]	public float spacing = 2.0f;				//The distance from one vertex to the next along any axis

        //GRID APPEARANCE
        [HideInInspector]	public bool showVertices = true;			//If true, the vertex objects of the polyGrid will be visible
        [HideInInspector]	public bool showEdgeRows = true;			//If true, the horizontal edges of the polyGrid will be visible
        [HideInInspector]	public bool showEdgeColumns = true;			//If true, the vertical edges of the polyGrid will be visible

        [HideInInspector]	public float vertexSize = 0.5f;				//The scale of all vertex objects
        [HideInInspector]	public float edgeThickness = 0.05f;			//The width of all edge objects

        [HideInInspector]	public Color vertexColour = Color.black;	//The colour that will be applied to all vertex objects
        [HideInInspector]	public Color edgeColour = Color.black;		//The colour that will be applied to all edge objects

        //GRID MOVEMENT
        [HideInInspector]	public float vertexRange = 0.5f;			//The maximum distance a vertex can move along any axis, positive or negative, away from its original position
        [HideInInspector]	public float minTime = 2.5f;				//The minimum number of seconds required for a vertex to move from one position to another
        [HideInInspector]	public float maxTime = 5.5f;				//The maximum number of seconds required for a vertex to move from one position to another
        [HideInInspector]	public float speedMultiplier = 1.0f;		//A multiplier that modifies the time a vertex requires to move from one position to another
        [HideInInspector]	public bool autoStart = true;				//If true, the PolyGrid will start moving on scene start, otherwise it will need to be triggered manually

        [System.Serializable]
        public class VertexRow
        {
            public List<Transform> vertices;							//A list of vertex objects that will make up a single row of the polyGrid
        }
        [HideInInspector]	public List<VertexRow> vertexRow;			//A list of all the vertex rows that make up the polyGrid object
        
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // BUILD POLY GRID
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void BuildGrid ()
        {
            Debug.Log("Creating new PolyGrid object - Rows " + rows + "// Columns " + columns + " // Spacing " + spacing);

            ClearGrid();																				                                    //Remove the existing PolyGrid objects from the scene

            //CREATE A CONTAINER FOR THE VERTICES - - - - - - - - - - - - - - - - - - - -
            GameObject vertexContainer = new GameObject();												                                    //Create a new object to be used as the parent for all the vertex objects
            vertexContainer.name = "VertexContainer";													                                    //Name the new object "VertexContainer"
            vertexContainer.transform.parent = this.transform;											                                    //Make the VertexContainer a child of this object

            int vertexCount = 0;																		                                    //Set the vertex count to zero

            vertexMaterial.color = vertexColour;														                                    //Apply the vertex colour to the vertex material

            //SPAWN AND POSITION THE VERTICES - - - - - - - - - - - - - - - - - - - -
            for (int x = 0; x < rows; x++)                                                                                                  //For each row in the PolyGrid...
            {

                vertexRow.Add(new VertexRow() );														                                    //...add a new list of vertices to the vertex row list...
                vertexRow[x].vertices = new List<Transform>();											                                    //...initialize the new list of vertices...

                for (int y = 0; y < columns; y++)                                                                                           //...cycle through each of the columns in the polyGrid...
                {
                    GameObject newVertex = (GameObject)Instantiate(vertex, this.transform);				                                    //......generate a new vertex prefab in the scene at the same position as this object...
                    newVertex.name = "Vertex." + vertexCount.ToString("0000");							                                    //......name the new object "Vertex." and add its number in the vertexCount...
                    newVertex.transform.parent = vertexContainer.transform;								                                    //......make the new vertex a child of the VertexContainer...
                    newVertex.transform.localScale *= vertexSize;										                                    //......apply the vertex size to the new vertex object...

                    newVertex.AddComponent<PolyVertex>();												                                    //......add a PolyVertex component to the new vertex object...
                    newVertex.GetComponent<PolyVertex>().vertexRange = vertexRange;						                                    //......apply the vertex range to the PolyVertex...
                    newVertex.GetComponent<PolyVertex>().minTime = minTime;								                                    //......apply the min time to the PolyVertex...
                    newVertex.GetComponent<PolyVertex>().maxTime = maxTime;								                                    //......apply the max time to the PolyVertex...
                    newVertex.GetComponent<PolyVertex>().polyGrid = this;								                                    //......set the PolyVertex to reference this PolyGrid component...

                    newVertex.GetComponent<Renderer>().material = vertexMaterial;						                                    //......apply the vertex material to the new vertex object...

                    newVertex.GetComponent<Renderer>().enabled = showVertices;							                                    //......enable or disable the new vertex's LineRenderer based on the showVertices toggle...

                    Vector2 offset = new Vector2( ((columns - 1) * spacing) / 2.0f, ((rows - 1) * spacing / 2.0f));				            //......create a new offset to position the vertex in the grid...
                    newVertex.transform.localPosition = new Vector3((y * spacing) - offset.x, 0.0f, (x * spacing) - offset.y);	            //......apply the offset to the local position of the new vertex...
                    vertexRow[x].vertices.Add(newVertex.transform);																            //......add the new vertex to the current vertex row array...

                    vertexCount++;																		                                    //......increment the vertex count by 1
                }
            }

            //CREATE A CONTAINER FOR THE LINES - - - - - - - - - - - - - - - - - - - -
            GameObject edgeContainer = new GameObject();												                                    //Create a new gameObject to be used as the parent for all the edge objects
            edgeContainer.name = "EdgeContainer";														                                    //Name the new object "EdgeContainer"
            edgeContainer.transform.parent = this.transform;											                                    //Make the EdgeContainer a child of this object

            edgeMaterial.color = edgeColour;															                                    //Apply the edge colour to the edge material

            //SPAWN AND POSITION THE ROW LINES - - - - - - - - - - - - - - - - - - - -
            for (int x = 0; x < rows; x++)                                                                                                  //Cycle through each of the rows of the polyGrid object...
            {
                GameObject newEdge = new GameObject();													                                    //...create a new gameObject...
                newEdge.name = "Edge.R." + x.ToString("00");											                                    //...name the new object "Edge.R." and add its number in the sequence...
                newEdge.transform.parent = edgeContainer.transform;										                                    //...make the new edge a child of the EdgeContainer...
                newEdge.transform.localPosition = Vector3.zero;											                                    //...reset the local position of the new edge object...

                newEdge.AddComponent<LineRenderer>();													                                    //...add a LineRenderer component to the new edge object...
                newEdge.GetComponent<LineRenderer>().positionCount = columns;							                                    //...use the number of columns in the polyGrid for the LineRenderer's position count...
                newEdge.GetComponent<LineRenderer>().widthMultiplier = edgeThickness;					                                    //...apply the edgeThickness to the LineRenderer's width multiplier...
                newEdge.GetComponent<LineRenderer>().useWorldSpace = false;								                                    //...set the LineRenderer to not use world space coordinates...

                newEdge.GetComponent<Renderer>().material = edgeMaterial;								                                    //...apply the edge material to the new edge object...

                newEdge.GetComponent<LineRenderer>().enabled = showEdgeRows;							                                    //...enable or disable the new edge's LineRenderer based on the showEdgeRows toggle...

                newEdge.AddComponent<PolyEdge>();														                                    //...add a PolyEdge component to the new edge object...
                newEdge.GetComponent<PolyEdge>().vertices = new List<Transform>();						                                    //...initialize the vertices list of the PolyEdge component...

                Vector3[] vertexPositions = new Vector3[columns];										                                    //...create a new array of positions as long as the number of columns in the polyGrid...

                for (int c = 0; c < columns; c++)                                                                                           //...cycle through each of the columns in the polyGrid object...
                {
                    vertexPositions[c] = vertexRow[x].vertices[c].localPosition;						                                    //......add the local position's of the vertex row to the vertex positions array...
                    newEdge.GetComponent<PolyEdge>().vertices.Add(vertexRow[x].vertices[c]);			                                    //......add the vertex row positions to the PolyEdge vertices list...
                }

                newEdge.GetComponent<LineRenderer>().SetPositions(vertexPositions);						                                    //...set the positions of the LineRenderer points using the vertex positions array
            }

            //SPAWN AND POSITION THE COLUMN LINES - - - - - - - - - - - - - - - - - - - -
            for (int y = 0; y < columns; y++)                                                                                               //Cycle through each of the columns of the polyGrid object...
            {
                GameObject newEdge = new GameObject();													                                    //...create a new gameObject...
                newEdge.name = "Edge.C." + y.ToString("00");											                                    //...name the new object "Edge.C." and add its number in the sequence...
                newEdge.transform.parent = edgeContainer.transform;										                                    //...make the new edge object a child of the EdgeContainer...
                newEdge.transform.localPosition = Vector3.zero;											                                    //...reset the local position of the new edge object...

                newEdge.AddComponent<LineRenderer>();													                                    //...add a LineRenderer component to the new edge object...
                newEdge.GetComponent<LineRenderer>().positionCount = rows;								                                    //...use the number of rows in the polyGrid for the LineRenderer's position count...
                newEdge.GetComponent<LineRenderer>().widthMultiplier = edgeThickness;					                                    //...apply the edgeThickness to the LineRenderer's width multiplier...
                newEdge.GetComponent<LineRenderer>().useWorldSpace = false;								                                    //...set the LineRenderer to not use world space coordinates...

                newEdge.GetComponent<Renderer>().material = edgeMaterial;								                                    //...apply the edge material to the new edge object...

                newEdge.GetComponent<LineRenderer>().enabled = showEdgeColumns;							                                    //...enable or disable the new edge's LineRenderer based on the showEdgeColumns toggle...

                newEdge.AddComponent<PolyEdge>();														                                    //...add a PolyEdge component to the new edge object...
                newEdge.GetComponent<PolyEdge>().vertices = new List<Transform>();						                                    //...initialize the vertices list of the PolyEdge component...

                Vector3[] vertexPositions = new Vector3[rows];											                                    //...create a new array of positions as long as the number of rows in the polyGrid...

                for (int r = 0; r < rows; r++)                                                                                              //...cycle through each of the rows in the polyGrid object...
                {
                    vertexPositions[r] = vertexRow[r].vertices[y].localPosition;						                                    //......add the local position's of the vertex row to the vertex positions array...
                    newEdge.GetComponent<PolyEdge>().vertices.Add(vertexRow[r].vertices[y]);			                                    //......add the vertex row positions to the PolyEdge vertices list...
                }

                newEdge.GetComponent<LineRenderer>().SetPositions(vertexPositions);						                                    //...set the positions of the LineRenderer points using the vertex positions array
            }
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // CLEAR VERTEX GRID - Remove all children of the polyGrid object from the scene
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void ClearGrid ()
        {
            for (int x = transform.childCount - 1; x >= 0; x--)                                                                             //Cycle through each child of this object...
            {
                DestroyImmediate(transform.GetChild(x).gameObject);										                                    //...and remove the child object from the scene
            }

            vertexRow.Clear();																			                                    //Clear the list of terrain tiles
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // ANIMATE GRID - Start moving the vertices of the grid
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void AnimateGrid ()
        {
            for (int x = 0; x < vertexRow.Count; x++)                                                                                       //Cycle through each vertex row...
            {
                for (int y = 0; y < vertexRow[x].vertices.Count; y++)                                                                       //...cycle through each vertex...
                {
                    vertexRow[x].vertices[y].GetComponent<PolyVertex>().MoveVertex();					                                    //......call the MoveVertex function to start the vertex moving
                }
            }
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // RESET GRID - Return all vertices to their original position's
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void ResetGrid ()
        {
            for (int x = 0; x < vertexRow.Count; x++)                                                                                       //Cycle through each vertex row...
            {
                for (int y = 0; y < vertexRow[x].vertices.Count; y++)                                                                       //...cycle through each vertex...
                {
                    vertexRow[x].vertices[y].GetComponent<PolyVertex>().ResetVertex();					                                    //......call the ResetVertex function to move the verex back to its original position
                }
            }
        }
    }
}
