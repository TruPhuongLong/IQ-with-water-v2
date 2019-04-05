﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
	public Transform prefab;
	float scale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < 200; i++)
		{
			
			for (int j = 0; j < 4; j++)
			{
				var paticle = Instantiate(prefab, new Vector3(
					prefab.position.x + scale * j, prefab.position.y + scale * i, prefab.position.z), 
					Quaternion.identity);
				paticle.gameObject.AddComponent<PaticleController>();
				paticle.parent = transform;

			}
		}
    }



}
