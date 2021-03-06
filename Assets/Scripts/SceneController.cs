﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	public GameObject liquid;
	public GameObject prefab;
	public GameObject funnel;
	public GameObject anchor_left_funnel;
	public GameObject anchor_right_funnel;
	public GameObject from;
	public GameObject to;
	public bool isTransformLiquid;

	GameObject parent;
	GameObject child1;
	GameObject child2;

	float parentWidthScale = 300;
	float childWidthScale = 120;
	float childHeightScaleUnit = 40;
	float padding = 0.5f;
	float paddingRight = 0.9f;

	Vector3 startFunnel;
	Vector3 startFrom;
	GameObject anchor_funnel;
	GameObject anchor_from;

	bool isRotateFromBottle;


	void InitBottles() {
		// init parent
		var (_parent, widthParent, heightParent) = FuncHelp.InitBottle(prefab,
			parentWidthScale, HeightScale.infinity, "Parent");
		parent = _parent;
		Vector3 worldPointParent = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 5));
		parent.transform.position = worldPointParent;
		parent.transform.Translate(widthParent / 2 + padding, padding, 0);

		//init child1
		var (_child1, widthChild1, heightChild1) = FuncHelp.InitBottle(prefab,
			childWidthScale, HeightScale.eleven, "Child1");
		child1 = _child1;
		Vector3 worldPointChild1 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 5));
		child1.transform.position = worldPointChild1;
		child1.transform.Translate((widthParent - widthChild1) / 2, padding, 0);

		//init child2
		var (_child2, widthChild2, heightChild2) = FuncHelp.InitBottle(prefab,
			childWidthScale, HeightScale.seven, "Child2");
		child2 = _child2;
		Vector3 worldPointChild2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 5));
		child2.transform.position = worldPointChild2;
		child2.transform.Translate(-(widthChild2 / 2 + paddingRight), padding, 0);

		// init point: top_center for parent, child:
		parent.GetComponent<BottleController>().top_center.transform.position = new Vector3(
			parent.transform.position.x, parent.transform.position.y + heightParent);
		child1.GetComponent<BottleController>().top_center.transform.position = new Vector3(
			child1.transform.position.x, child1.transform.position.y + heightChild1);
		child2.GetComponent<BottleController>().top_center.transform.position = new Vector3(
			child2.transform.position.x, child2.transform.position.y + heightChild2);

		//init anchor point for rotate bottle
		parent.GetComponent<BottleController>().top_left.transform.position = new Vector3(parent.transform.position.x - widthParent / 2,
			parent.transform.position.y + heightParent * 4 / 5);
		parent.GetComponent<BottleController>().top_right.transform.position = new Vector3(parent.transform.position.x + widthParent / 2,
			parent.transform.position.y + heightParent * 4 / 5);
		child1.GetComponent<BottleController>().top_left.transform.position = new Vector3(child1.transform.position.x - widthChild1 / 2,
			child1.transform.position.y + heightChild1 * 4 / 5);
		child1.GetComponent<BottleController>().top_right.transform.position = new Vector3(child1.transform.position.x + widthChild1 / 2,
			child1.transform.position.y + heightChild1 * 4 / 5);
		child2.GetComponent<BottleController>().top_left.transform.position = new Vector3(child2.transform.position.x - widthChild2 / 2,
			child2.transform.position.y + heightChild2 * 4 / 5);
		child2.GetComponent<BottleController>().top_right.transform.position = new Vector3(child2.transform.position.x + widthChild2 / 2,
			child2.transform.position.y + heightChild2 * 4 / 5);

		//declare volumetric of each bottle
		parent.GetComponent<BottleController>().volumetricSource
			= HeightScale.infinity;
		child1.GetComponent<BottleController>().volumetricSource
			= HeightScale.eleven;
		child2.GetComponent<BottleController>().volumetricSource
			= HeightScale.seven;

		// init liquid:
		parent.GetComponent<BottleController>().realyVolumetricSource
			= (int)HeightScale.infinity;
		child1.GetComponent<BottleController>().realyVolumetricSource = 0;
		child2.GetComponent<BottleController>().realyVolumetricSource = 0;
	}

	void TranslateFunnelTo(Vector3 position) {
		funnel.transform.position = position;
	}

	void ResetPosition() {
		funnel.transform.position = startFunnel;
		from.transform.position = startFrom;
	}

	void getAnchorPoint() {
		anchor_funnel =
			to == parent ? anchor_right_funnel :
			to == child2 ? anchor_left_funnel :
			from == parent ? anchor_left_funnel :
			anchor_right_funnel;

		anchor_from =
			anchor_funnel == anchor_right_funnel ?
			from.GetComponent<BottleController>().top_left :
			from.GetComponent<BottleController>().top_right;
	}

	void TranslateBottle() {
		getAnchorPoint();
		var delta = anchor_funnel.transform.position - anchor_from.transform.position;
		print("from top_left " + from.GetComponent<BottleController>().top_left);

		from.transform.Translate(
			delta.x,
			delta.y,
			0);
	}

	void RotateBottle() {
		from.transform.RotateAround(anchor_from.transform.position, Vector3.forward, -20 * Time.deltaTime);
	}


	public IEnumerator TransformLiquid() {
		isTransformLiquid = true;

		startFrom = from.transform.position;

		//translate funnel:
		TranslateFunnelTo(to.GetComponent<BottleController>().top_center.transform.position);

		//translate bottle
		TranslateBottle();

		//rotate bottle
		isRotateFromBottle = true;

		
		yield return new WaitForSeconds(5);
		isRotateFromBottle = false;

		//wait 5 second and reset everything
		yield return new WaitForSeconds(10);
		ResetPosition();
		from.GetComponent<BottleController>().reset();
		to.GetComponent<BottleController>().reset();

		from = null;
		to = null;

		isTransformLiquid = false;
	}

	






	private void Awake()
	{
		startFunnel = funnel.transform.position;
		InitBottles();
	}

	private void Update()
	{
		if (isRotateFromBottle)
		{
			RotateBottle();
		}
	}


}
