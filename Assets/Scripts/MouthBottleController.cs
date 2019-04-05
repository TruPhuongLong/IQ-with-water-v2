using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthBottleController : MonoBehaviour
{
	SceneController sceneController;

	void initSceneController()
	{
		if (sceneController == null)
		{
			sceneController = GameObject.FindWithTag("SceneController")
			.GetComponent<SceneController>();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		initSceneController();


		if (collision.tag == "liquid" && collision.GetComponent<PaticleController>() != null)
		{
			collision.GetComponent<PaticleController>().status =
				collision.GetComponent<PaticleController>().status
				== PaticleController.Status.outerBottle ?
				PaticleController.Status.innerBottle : PaticleController.Status.outerBottle;

			if (collision.GetComponent<PaticleController>().status
				== PaticleController.Status.innerBottle)
			{
				// add this paticle to bottle:
				collision.GetComponent<Transform>().SetParent(transform.parent.transform);
			}
			else
			{
				//remove this paticle from bottle:
				collision.GetComponent<Transform>().SetParent(null);
			}
		}
	}
}
