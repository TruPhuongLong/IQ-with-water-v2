using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncHelp: MonoBehaviour
{
	static float thickScale = 20f;
	static float childHeightScaleUnit = 40;
	float parentHeightScale = 500;

	public static (GameObject, float, float) InitBottle(GameObject prefab,
		float widthScale, HeightScale heightScale, string name)
	{
		// bottom
		var bottom = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
		bottom.transform.localScale = new Vector3(widthScale, thickScale, 0);
		bottom.GetComponent<SpriteRenderer>().color = Color.black;

		//left
		var left = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
		left.transform.localScale = new Vector3(thickScale, 
			(float)heightScale * childHeightScaleUnit, 0);
		left.GetComponent<SpriteRenderer>().color = Color.black;

		//right
		var right = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
		right.transform.localScale = new Vector3(thickScale,
			(float)heightScale * childHeightScaleUnit, 0);
		right.GetComponent<SpriteRenderer>().color = Color.black;

		//translate left and right
		var thickBottle = bottom.GetComponent<SpriteRenderer>().bounds.size.y;
		var widthBottle = bottom.GetComponent<SpriteRenderer>().bounds.size.x;
		var heightBottle = left.GetComponent<SpriteRenderer>().bounds.size.y;
		left.transform.Translate(new Vector3(-widthBottle / 2 + thickBottle / 2, 0, 0));
		left.transform.Translate(new Vector3(0, heightBottle / 2 - thickBottle / 2, 0));
		right.transform.Translate(new Vector3(widthBottle / 2 - thickBottle / 2, 0, 0));
		right.transform.Translate(new Vector3(0, heightBottle / 2 - thickBottle / 2, 0));

		// add boxCollider to contain water:
		bottom.AddComponent<BoxCollider2D>();
		left.AddComponent<BoxCollider2D>();
		right.AddComponent<BoxCollider2D>();

		//add to parent
		// translate parent mean: change pivot point (0.5, 0)
		var parent = new GameObject(name);
		parent.transform.Translate(0, -thickBottle / 2, 0);
		bottom.transform.parent = parent.transform;
		left.transform.parent = parent.transform;
		right.transform.parent = parent.transform;

		//add BoxCollider2D to Parent
		parent.AddComponent<BoxCollider2D>();
		parent.GetComponent<BoxCollider2D>().size = new Vector2(widthBottle, heightBottle);
		parent.GetComponent<BoxCollider2D>().offset = new Vector2(0, heightBottle / 2);
		parent.GetComponent<BoxCollider2D>().isTrigger = true;
		parent.AddComponent<BottleController>();

		return (parent, widthBottle, heightBottle);
	}
}

public enum HeightScale
{
	none = 0,
	infinity = 13,
	eleven = 11,
	seven = 7
}
