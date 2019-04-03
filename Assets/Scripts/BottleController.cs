using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleController : MonoBehaviour
{
	int count = 0;
	

	public HeightScale volumetricSource;

	public float realyVolumetricSource;

	BottleController target;

	private void OnMouseDown()
	{
		count++;

		if (count % 2 != 0)
		{
			Debug.Log("chose");
			setColorForBottle(Color.green);
		}
		else {
			Debug.Log("destroy");
			setColorForBottle(Color.black);
		}
	}


	void setColorForBottle(Color color) {
		for (int i = 0; i < 3; i++)
		{
			transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
		}
	}

}
