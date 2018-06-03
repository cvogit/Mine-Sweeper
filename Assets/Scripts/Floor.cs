using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The floor object.
/// Hold the state of the game.
/// </summary>
public class Floor : MonoBehaviour {

	public Sprite FloorDefault;
	public Sprite FloorEmpty;
	public Sprite FloorMine;
	public Sprite FloorFlag;

	public SpriteRenderer FloorRender;
	public Text FloorText;

	public GameController mGameController;
	public FlagButton mFlagButton;

	public int XCoordinate;
	public int YCoordinate;
	public bool IsFlipped;

	private Mine mMine;
	private int SurroundMines;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		mMine = null;
		IsFlipped = false;
	}

	/// <summary>
	/// Determines whether this instance has mine.
	/// </summary>
	/// <returns><c>true</c> if this instance has mine; otherwise, <c>false</c>.</returns>
	public bool HasMine() {
		if (mMine != null)
			return true;
		return false;
	}

	/// <summary>
	/// Sets the mine.
	/// </summary>
	public void SetMine() {
		mMine = new Mine();
	}

	/// <summary>
	/// Raises the mouse up as button event.
	/// </summary>
	void OnMouseUpAsButton()
	{
		// Check if the flag button is being held
		// Flag or unflag the floor
		if (mFlagButton.IsActive ()) {
			if(FloorRender.sprite != FloorFlag)
				FloorRender.sprite = FloorFlag;
			else if(FloorRender.sprite == FloorFlag)
				FloorRender.sprite = FloorDefault;
		}
		else {
			// If the floor is not being flagged, flip it
			if (FloorRender.sprite != FloorFlag) {
				// If the floor has a mine, end the game
				if (HasMine ()) {
					FloorRender.sprite = FloorMine;
					mGameController.GameOver ();
				} else {
					FlipFloor ();
				}
			}
		}
	}

	/// <summary>
	/// Flips the floor and spread to neighbor if there are no mines adjacent.
	/// </summary>
	public void FlipFloor() {
		IsFlipped = true;
		FloorRender.sprite = FloorEmpty;

		if (SurroundMines > 0)
			FloorText.text = SurroundMines.ToString ();
		else
			FlipSurroundingFloors ();
	}

	/// <summary>
	/// Sets the surrounding mines number.
	/// </summary>
	public void SetSurroundingMinesNumber() {
		// Get the current floor index
		SurroundMines = 0;

		// Check the possible 8 adjacent floors for mines
		if (mGameController.CheckFloorForMine(XCoordinate - 1, YCoordinate - 1))
			SurroundMines++;
		if (mGameController.CheckFloorForMine(XCoordinate - 1, YCoordinate))
			SurroundMines++;
		if (mGameController.CheckFloorForMine(XCoordinate - 1, YCoordinate + 1))
			SurroundMines++;
		if (mGameController.CheckFloorForMine(XCoordinate, YCoordinate - 1))
			SurroundMines++;
		if (mGameController.CheckFloorForMine(XCoordinate, YCoordinate + 1))
			SurroundMines++;
		if (mGameController.CheckFloorForMine(XCoordinate + 1, YCoordinate - 1))
			SurroundMines++;
		if (mGameController.CheckFloorForMine(XCoordinate + 1, YCoordinate))
			SurroundMines++;
		if (mGameController.CheckFloorForMine(XCoordinate + 1, YCoordinate + 1))
			SurroundMines++;
	}

	/// <summary>
	/// Flips the surrounding floors.
	/// </summary>
	public void FlipSurroundingFloors() {
		mGameController.QueueFlipFloor (XCoordinate - 1, YCoordinate - 1);
		mGameController.QueueFlipFloor (XCoordinate - 1, YCoordinate);
		mGameController.QueueFlipFloor (XCoordinate - 1, YCoordinate + 1);
		mGameController.QueueFlipFloor (XCoordinate, YCoordinate - 1);
		mGameController.QueueFlipFloor (XCoordinate, YCoordinate + 1);
		mGameController.QueueFlipFloor (XCoordinate + 1, YCoordinate - 1);
		mGameController.QueueFlipFloor (XCoordinate + 1, YCoordinate);
		mGameController.QueueFlipFloor (XCoordinate + 1, YCoordinate + 1);
	}
}
