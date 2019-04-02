using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	public GameObject prefab;
	float parentWidthScale = 300;
	float parentHeightScale = 500;
	float childWidthScale = 120;
	float childHeightScaleUnit = 40;
	float child1HeightScale = 440;
	float child2HeightScale = 280;
	float padding = 0.5f;

	void initBottle() {
		var (parent, widthParent, _) = FuncHelp.InitBottle(prefab,
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
		child2.transform.Translate(-(widthChild2 / 2 + padding), padding, 0);
	}

	private void Awake()
	{
		initBottle();
	}


}
