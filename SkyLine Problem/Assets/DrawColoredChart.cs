using UnityEngine;
using System;
using System.Collections;

public class DrawColoredChart : MonoBehaviour
{
	public GameObject unitPrefab;
	public Transform parent;
	public Color[] colors;


	private int colorIndex = 0;

	private void Start()
	{
		colorIndex = UnityEngine.Random.Range(0, colors.Length);
		StartDrawing("(1,11,3);(2,5,6);(3,6,4);(8,15,10);(12,2,13);(14,15,15)");
	}

	public void StartDrawing(string input)
	{
		StartCoroutine(Draw(input));
	}

	public IEnumerator Draw(string input)
	{
		DeleteAllChart();
		string[] cors = input.Split(';');
		if (cors.Length > 0)
		{
			foreach (string cor in cors)
			{
				Color current = colors[(colorIndex++)%colors.Length];
				string corNew = cor.Substring(1, cor.Length - 2);
				Debug.Log(corNew);

				string[] corsString = corNew.Split(',');
				if (corsString.Length == 3)
				{
					float left, height, right;
					left = float.Parse(corsString[0]) * 0.16f - 0.16f;
					height = float.Parse(corsString[1]);
					right = float.Parse(corsString[2]);
					right = (right < 15) ? right : 14;
					right *= 0.16f;
					for (float i = left; i < right; i += 0.16f)
					{
						for (int j = 0; j < height; j++)
						{
							GameObject obj = Instantiate(unitPrefab, parent);
							obj.transform.localPosition = new Vector3(i, j * 10, 0);
							obj.GetComponent<SpriteRenderer>().color = current;
							obj.name = "unit_" + cor + "_" + obj.transform.localPosition.y/10;
							yield return new WaitForSeconds(0.03f);
						}
						yield return new WaitForSeconds(0.5f);
					}
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
