using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleController : MonoBehaviour
{
	int count = 0;

	public HeightScale volumetricSource;

	public float realyVolumetricSource;

	SceneController sceneController;

	public GameObject top_center;

	public GameObject top_left;

	public GameObject top_right;

	public List<GameObject> listLiquidPaticle;

	private void Awake()
	{
		top_center = new GameObject("top_center");
		top_left = new GameObject("top_left");
		top_right = new GameObject("top_right");
	}


	public void reset()
	{
		count = 0;
		setColorForBottle(Color.black);
	}

	private void OnMouseDown()
	{
		//find scene controller
		if (sceneController == null)
		{
			sceneController = GameObject.FindWithTag("SceneController")
			.GetComponent<SceneController>();
		}

		//if liquid is transforming, return and do nothing
		if (sceneController.isTransformLiquid)
		{
			return;
		}

		count++;

		// change color
		if (count % 2 != 0)
		{
			setColorForBottle(Color.green);
			if (sceneController.from == null)
			{
				sceneController.from = gameObject;
			}
			else
			{
				sceneController.to = gameObject;

				//check:
				var bottleFrom = sceneController.from.GetComponent<BottleController>();
				var bottleTo = sceneController.to.GetComponent<BottleController>();
				if ((bottleTo.realyVolumetricSource 
					>= (float)bottleTo.volumetricSource)
					|| bottleFrom.realyVolumetricSource <= 0)
				{
					bottleFrom.reset();
					bottleTo.reset();
					sceneController.from = null;
					sceneController.to = null;
					return;
				}

				var deltaTarget = (float)bottleTo.volumetricSource - bottleTo.realyVolumetricSource;
				var trans = Mathf.Min(deltaTarget, bottleFrom.realyVolumetricSource);

				//set realy volumetric for both source and target:
				bottleTo.realyVolumetricSource += trans;
				bottleFrom.realyVolumetricSource -= trans;

				//start tranform liquid 
				StartCoroutine(sceneController.TransformLiquid());
			}
		}
		else {
			setColorForBottle(Color.black);
			sceneController.from = null;
		}

		

	}


	void setColorForBottle(Color color) {
		for (int i = 0; i < 3; i++)
		{
			transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
		}
	}

	

}
