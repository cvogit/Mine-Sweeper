using UnityEngine;

/// <summary>
/// Pinch to zoom in and out of the board.
/// Modified from https://answers.unity.com/questions/1271122/pinch-zoom-on-ui.html
/// </summary>
public class PinchToZoom : MonoBehaviour
{
	public Canvas canvas; // The canvas
	public float zoomSpeed = 0.5f;        // The rate of change of the canvas scale factor

	void Update()
	{
		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			// ... change the canvas size based on the change in distance between the touches.
			canvas.scaleFactor -= deltaMagnitudeDiff * zoomSpeed;

			// Make sure the canvas size stay between 0.5f and 3.0f
			canvas.scaleFactor = Mathf.Max(canvas.scaleFactor, 1.0f);
			canvas.scaleFactor = Mathf.Min(canvas.scaleFactor, 3.0f);

		}
	}
}