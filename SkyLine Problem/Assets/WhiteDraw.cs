using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteDraw : MonoBehaviour {

	public GameObject unitPrefab;
	public Transform parent;

	private int colorIndex = 0;

	private void Start()
	{
		//StartDrawing("(1,11,3,15,4,17,6,8,7,12,8,15,9,20,13,5)");
	}

	public void StartDrawing(string input)
	{
		StartCoroutine(Draw(input));
	}

	public IEnumerator Draw(string input)
	{
		DeleteAllChart();

		string[] cors = input.Substring(1, input.Length - 2).Split(',');
		int[] lefts = new int[cors.Length / 2];
		int[] heights = new int[cors.Length / 2];

		for (int i = 0; i < cors.Length; i++)
		{
			if(i % 2 == 0)
			{
				lefts[i / 2] = int.Parse(cors[i]);
				Debug.Log(lefts.ToString());
			}
			else
			{
				heights[i / 2] = int.Parse(cors[i]);
				Debug.Log(heights.ToString());
			}
		}

		if (heights.Length > 0)
		{
			for (int k = 0; k < lefts.Length; k++)
			{
				float left = lefts[k] * 0.16f - 0.16f;
				float height = heights[k];
				for (float i = left; i < lefts[k+1]; i += 0.16f)
				{
					for (int j = 0; j < height; j++)
					{
						GameObject obj = Instantiate(unitPrefab, parent);
						obj.transform.localPosition = new Vector3(i, j * 10, 0);
						obj.GetComponent<SpriteRenderer>().color = Color.white;
						obj.name = "unit_" + left + "_" + obj.transform.localPosition.y / 10;
						yield return new WaitForSeconds(0.03f);
					}
					yield return new WaitForSeconds(0.5f);
				}
			}
		}
	}

	void DeleteAllChart()
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			if (parent.GetChild(i).name.Contains("unit"))
				Destroy(parent.GetChild(i).gameObject);
		}
	}
}
