using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 						Ripcord Tools, Copyright © 2018, Ripcord Development
//										     ColourSlider.cs
//												 v1.0.0
//										   info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - This script selects a colour from a gradient based on the position of the slider.

namespace Ripcord.Common
{
	public class ColourSlider : MonoBehaviour
    {
		public Image colourSwatch;

		Gradient gradient;
		Slider slider;

		void Start ()
        {
			slider = this.GetComponent<Slider>();

			gradient = new Gradient();
			GradientColorKey[] colourKey = new GradientColorKey[5];

			colourKey[0].color = Color.white;
			colourKey[0].time = 0.0f;
			colourKey[1].color = Color.red;
			colourKey[1].time = 0.25f;
			colourKey[2].color = Color.green;
			colourKey[2].time = 0.5f;
			colourKey[3].color = Color.blue;
			colourKey[3].time = 0.75f;
			colourKey[4].color = Color.black;
			colourKey[4].time = 1.0f;

			GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
			alphaKey[0].alpha = 1.0f;
			alphaKey[0].time = 0.0f;
			alphaKey[1].alpha = 1.0f;
			alphaKey[1].time = 1.0f;

			gradient.SetKeys(colourKey, alphaKey);

			SetColour();
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		// SET COLOUR - Select a colour from the gradient based on the position of the slider
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		public void SetColour ()
        {
			colourSwatch.color = gradient.Evaluate(slider.value);
		}
	}
}