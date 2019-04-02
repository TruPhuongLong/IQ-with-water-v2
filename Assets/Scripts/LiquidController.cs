using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
	public Transform prefab;
	float scale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < 100; i++)
		{
			
			for (int j = 0; j < 5; j++)
			{
				var newLiquid = Instantiate(prefab, new Vector3(
					prefab.position.x + scale * j, prefab.position.y + scale * i, prefab.position.z), 
					Quaternion.identity);
				newLiquid.transform.parent = transform;

			}
		}
    }

}
