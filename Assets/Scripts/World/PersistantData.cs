using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PersistantData {

	#region color definitions
	public static Color Orange = new Color(1f, .5f, 0f, 1f);
	public static Color Red = Color.red;
	public static Color Yellow = Color.yellow;
	public static Color Green = Color.green;
	public static Color Purple = new Color(.5f, 0f, .6f, 1f);
	public static Color Pink = new Color(1f, 0f, 1f, 1f);
	#endregion

	public static Color[] colors = {Orange, Red, Yellow, Green, Purple, Pink};
	public static string[] colorStrings = {"Orange","Red","Yellow","Green","Purple","Pink"};
	public static Dictionary<string, int> colorNumbers = new Dictionary<string, int>{
							{"Orange", 0},
							{"Red", 1},
							{"Yellow", 2},
							{"Green", 3},
							{"Purple", 4},
							{"Pink", 5}
	};
	public static int[] indexToPlayer = new int[4];

	public static List<int> playersToSpawn = new List<int>(); /*TEST new List<int>{0, 1};*/

	public static Dictionary<int, int> mostRecentScores = new Dictionary<int, int>();
}
