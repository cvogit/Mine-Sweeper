using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The floor object.
/// Hold the state of the game.
/// </summary>
public class Floor : MonoBehaviour {

	/// <summary>
	/// True if floor have mine underneath.
	/// </summary>
	private Mine mMine;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		mMine = null;
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

}
