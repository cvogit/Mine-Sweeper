using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game controller.
/// Controll the game loop.
/// </summary>
public class GameController : MonoBehaviour {

	public GameObject Menu;

	public int MineNumber;

	public List<GameObject> mFloors;
	private List<GameObject> FloorFlipQueue;

	private List<List<GameObject>> mFloorArray;

	void Awake() {

		// Initialize the 2D mine list
		mFloorArray = new List<List<GameObject>>();
		int tCount = 0;

		for (int x = 0; x < 8; x++) {
			// Initialize a floor row
			List<GameObject> tFloorRow = new List<GameObject>();
			for (int y = 0; y < 8; y++) {
				// Set the floor x and y coordinates
				mFloors[tCount].GetComponent<Floor>().XCoordinate = x;
				mFloors[tCount].GetComponent<Floor>().YCoordinate = y;

				// Add 1 flood into the row
				tFloorRow.Add(mFloors[tCount]);
				tCount++;
			}
			// Add the row in mine 2D array
			mFloorArray.Add (tFloorRow);
		}

		FloorFlipQueue = new List<GameObject> ();
		SetBoard ();
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {
		foreach (GameObject tFloor in mFloors) {
			tFloor.GetComponent<Floor> ().SetSurroundingMinesNumber ();
		}
	}

	/// <summary>
	/// Gets the floor.
	/// </summary>
	/// <returns>The floor.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public GameObject GetFloor(int pX, int pY) {
		if ( (pX < 0 || pX > 7) || (pY < 0 || pY > 7) )
			return null;
		return mFloorArray [pX] [pY];
	}

	/// <summary>
	/// Sets the board.
	/// </summary>
	public void SetBoard() {

		int mineCount = MineNumber;
		int tIndex;
		GameObject tFloor;

		// Place the mines
		while (mineCount > 0) {
			tIndex = Random.Range (0, 64);
			tFloor = mFloors[tIndex];
			if (!tFloor.GetComponent<Floor> ().HasMine ()) {
				tFloor.GetComponent<Floor> ().SetMine ();
				mineCount--;
			}
		}
		// TODO consider using Simon Tatham's solvable mine generator algorithm
	}

	/// <summary>
	/// Checks the floor for mine.
	/// </summary>
	/// <returns><c>true</c>, if floor for mine was checked, <c>false</c> otherwise.</returns>
	/// <param name="pIndex">P index.</param>
	public bool CheckFloorForMine(int pX, int pY) {
		// If the indexes are out of range, return false
		if ( (pX < 0 || pX > 7) || (pY < 0 || pY > 7) )
			return false;

		GameObject tFloor = GetFloor (pX, pY);
		if(tFloor)
			return tFloor.GetComponent<Floor> ().HasMine ();
		return false;
	}

	/// <summary>
	/// Flips the floor.
	/// </summary>
	/// <param name="pFloor">P floor.</param>
	public void FlipFloor(GameObject pFloor) {
		pFloor.GetComponent<Floor> ().FlipFloor ();
	}

	/// <summary>
	/// Queues the flip floor.
	/// </summary>
	/// <param name="pX">P x.</param>
	/// <param name="pY">P y.</param>
	public void QueueFlipFloor(int pX, int pY) {
		// If the indexes are out of range, return 
		if ( (pX < 0 || pX > 7) || (pY < 0 || pY > 7) )
			return;
		
		GameObject tFloor = GetFloor (pX, pY);
		if( !tFloor.GetComponent<Floor>().IsFlipped )
			FloorFlipQueue.Add (tFloor);
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update() {
		if (FloorFlipQueue.Count > 0) {
			FloorFlipQueue[0].GetComponent<Floor>().FlipFloor();
			FloorFlipQueue.RemoveAt (0);
		}
	}

	public void GameOver() {
		Vector3 tPosition = Menu.transform.position;
		tPosition.x = 0;
		Menu.transform.position = tPosition;
	}
}
