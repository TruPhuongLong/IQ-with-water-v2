using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseController : MonoBehaviour
{
	int count = 0;
	SceneController sceneController;

	void initSceneController() {
		if (sceneController == null)
		{
			sceneController = GameObject.FindWithTag("SceneController")
			.GetComponent<SceneController>();
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		initSceneController();

		var liquidController = sceneController.liquid.GetComponent<LiquidController>();
		var index = liquidController.listLiquidPaticle.IndexOf(collision.GetComponent<Transform>());
		if (index != -1)
		{
			liquidController.listLiquidPaticle.RemoveAt(index);
		}
		count++;
		Debug.Log("=== count OnTriggerEnter2D: " + count + 
			" index of paticle: " + index + " list count: " + liquidController.listLiquidPaticle.Count);
		Destroy(collision);
	}
}
