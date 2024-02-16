using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 						Ripcord Tools, Copyright © 2018, Ripcord Development
//										    TextureSlider.cs
//												 v1.0.0
//										   info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - This script selects a texture from a list based on the position of the slider.

namespace Ripcord.Common
{
	public class TextureSlider : MonoBehaviour
    {
		public Image textureSwatch;

		public List<Texture2D> textures;
		public Texture2D selectedTexture;

		Slider slider;

		void Awake ()
        {
			slider = this.GetComponent<Slider>();

			SetTexture();
		}

		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		// SET TEXTURE - Select a texture from the list based on the position of the slider
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		public void SetTexture ()
        {
			selectedTexture = textures[(int)slider.value];
			textureSwatch.sprite = Sprite.Create(selectedTexture, new Rect(0.0f, 0.0f, selectedTexture.width, selectedTexture.height), new Vector2(0.5f, 0.5f));
		}
	}
}