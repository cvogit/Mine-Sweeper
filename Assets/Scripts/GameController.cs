using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game controller.
/// Controll the game loop.
/// </summary>
public class GameController : MonoBehaviour {

	public List<GameObject> mFloors;

	private List<List<GameObject>> mFloorArray;

	void Awake() {

		// Initialize the 2D mine list
		mFloorArray = new List<List<GameObject>>();
		int tCount = 0;

		for (int i = 0; i < 8; i++) {
			// Initialize 1 Floor row
			List<GameObject> tFloorRow = new List<GameObject>();
			for (int y = 0; y < 8; y++) {
				// Add 1 mine into the row
				tFloorRow.Add(mFloors[tCount]);
				tCount++;
			}
			// Add the row in mine 2D array
			mFloorArray.Add (tFloorRow);
		}
	}

	void Start() {
		Debug.Log (GetFloor (7, 7));
		Debug.Log (GetFloor (6, 7));

	}

	/// <summary>
	/// Gets the floor.
	/// </summary>
	/// <returns>The floor.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public GameObject GetFloor(int x, int y) {
		if( (mFloorArray.Count * y) > (x * y) )
			return mFloorArray [x] [y];
		return null;
	}
}
