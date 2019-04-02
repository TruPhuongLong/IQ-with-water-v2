using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	public GameObject prefab;
	public GameObject funnel;

	float parentWidthScale = 300;
	float parentHeightScale = 500;
	float childWidthScale = 120;
	float childHeightScaleUnit = 40;
	float child1HeightScale = 440;
	float child2HeightScale = 280;
	float padding = 0.5f;
	float paddingRight = 0.9f;

	Vector2 top_center_parent;
	Vector2 top_center_child1;
	Vector2 top_center_child2;

	void initBottle() {
		var (parent, widthParent, heightParent) = FuncHelp.InitBottle(prefab,
			parentWidthScale, parentHeightScale, "Parent");
		Vector3 worldPointParent = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 5));
		parent.transform.position = worldPointParent;
		parent.transform.Translate(widthParent / 2 + padding, padding, 0);

		var (child1, widthChild1, heightChild1) = FuncHelp.InitBottle(prefab,
			childWidthScale, child1HeightScale, "Child1");
		Vector3 worldPointChild1 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 5));
		child1.transform.position = worldPointChild1;
		child1.transform.Translate((widthParent - widthChild1) / 2, padding, 0);

		var (child2, widthChild2, heightChild2) = FuncHelp.InitBottle(prefab,
			childWidthScale, child2HeightScale, "Child1");
		Vector3 worldPointChild2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 5));
		child2.transform.position = worldPointChild2;
		child2.transform.Translate(-(widthChild2 / 2 + paddingRight), padding, 0);

		// init top_center for parent, child:
		top_center_parent = new Vector2(
			parent.transform.position.x, parent.transform.position.y + heightParent);
		top_center_child1 = new Vector2(
			child1.transform.position.x, child1.transform.position.y + heightChild1);
		top_center_child2 = new Vector2(
			child2.transform.position.x, child2.transform.position.y + heightChild2);
	}

	void abc() {
		funnel.transform.position = top_center_parent;
	}

	

	private void Awake()
	{
		initBottle();
		abc();
	}


}
