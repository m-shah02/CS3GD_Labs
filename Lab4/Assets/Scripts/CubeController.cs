using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	Color[] colours;
	int colourIndex;
	MeshRenderer renderer;

	void Awake() 
	{
		// fill array with colours
		colours = new Color[5];
		colours[0] = Color.blue;
		colours[1] = Color.red;
		colours[2] = Color.green;
		colours[3] = Color.yellow;
		colours[4] = Color.cyan;

		// set index to random colour and update material
		colourIndex = Random.Range (0, colours.Length - 1);
		renderer = GetComponent<MeshRenderer> ();
		UpdateMaterial ();
	}

	void UpdateMaterial() 
	{
		// update the colour of the renderer's material
		renderer.material.color = colours [colourIndex];
	}
	
	public void ChangeColour() 
	{
		// select next colour in the array and then update material
		colourIndex++;
		if (colourIndex == colours.Length) colourIndex = 0;
		UpdateMaterial ();
	}

}
