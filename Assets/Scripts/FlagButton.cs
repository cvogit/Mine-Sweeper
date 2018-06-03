using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Flag button.
/// Script for mobile devices
/// </summary>
public class FlagButton : MonoBehaviour {

	public SpriteRenderer ButtonRender;

	public Sprite PressedSprite;
	public Sprite UnPressedSprite;

	private bool isPressed;

	void Awake() {
		isPressed = false;
	}

	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
	void OnMouseDown(){
		isPressed = !isPressed;
		if (isPressed)
			ButtonRender.sprite = PressedSprite;
		else
			ButtonRender.sprite = UnPressedSprite;

	}

	/// <summary>
	/// Determines whether this instance is being pressed.
	/// </summary>
	/// <returns><c>true</c> if this instance is being pressed; otherwise, <c>false</c>.</returns>
	public bool IsActive() {
		return isPressed;
	}
}
