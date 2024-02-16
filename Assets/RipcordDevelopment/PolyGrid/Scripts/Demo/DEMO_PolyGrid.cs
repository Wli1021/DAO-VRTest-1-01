using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 						    PolyGrid 1.0, Copyright © 2018, Ripcord Development
//											DEMO_PolyGrid.cs
//										   info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - This script connects the UI in the demo scene to the values of the connected PolyGrid object

namespace PolyGrid
{
    public class DEMO_PolyGrid : MonoBehaviour {

        public PolyGrid polyGrid;

        [Header ("Grid Dimensions")]
        public Slider rowCount;
        public Slider columnCount;
        public Slider vertexSpacing;

        [Header ("Grid Appearance")]
        public Toggle showVertices;
        public Toggle showEdgeRows;
        public Toggle showEdgeColumns;
        public Slider vertexSize;
        public Slider edgeThickness;
        public Ripcord.Common.TextureSlider vertexShape;
        public Image vertexColour;
        public Image edgeColour;

        [Header ("Grid Movement")]
        public Slider vertexRange;
        public Slider speedMultiplier;

        void Start ()
        {
            SetRowCount();
            SetColumnCount();
            SetVertexSpacing();
            ToggleVertices();
            ToggleEdgeRows();
            ToggleEdgeColumns();
            SetVertexSize();
            SetEdgeThickness();
            SetVertexShape();
            SetVertexColour();
            SetEdgeColour();
            SetVertexRange();
            SetSpeedMultiplier();
        }
        
        //GRID DIMENSIONS - - - - - - - - - - - - - - - - - - - -
        public void SetRowCount () {			polyGrid.rows = (int)rowCount.value;									}
        public void SetColumnCount () {			polyGrid.columns = (int)columnCount.value;								}
        public void SetVertexSpacing () {		polyGrid.spacing = vertexSpacing.value;									}

        //GRID APPEARANCE - - - - - - - - - - - - - - - - - - - -
        public void ToggleVertices () {			polyGrid.showVertices = showVertices.isOn;								}
        public void ToggleEdgeRows () {			polyGrid.showEdgeRows = showEdgeRows.isOn;								}
        public void ToggleEdgeColumns () {		polyGrid.showEdgeColumns = showEdgeColumns.isOn;						}
        public void SetVertexSize () {			polyGrid.vertexSize = vertexSize.value;									}
        public void SetEdgeThickness () {		polyGrid.edgeThickness = edgeThickness.value;							}
        public void SetVertexShape () {			polyGrid.vertexMaterial.mainTexture = vertexShape.selectedTexture;		}
        public void SetVertexColour () {		polyGrid.vertexColour = vertexColour.color;								}
        public void SetEdgeColour () {			polyGrid.edgeColour = edgeColour.color;									}

        //GRID MOVEMENT - - - - - - - - - - - - - - - - - - - -
        public void SetVertexRange () {			polyGrid.vertexRange = vertexRange.value;								}
        public void SetSpeedMultiplier () {		polyGrid.speedMultiplier = speedMultiplier.value;						}
    }
}
