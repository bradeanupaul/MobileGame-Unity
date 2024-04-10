using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private Vector3 cameraInitialPosition;

	public float shakeMagnitude = 0.7f;

	public float shakeTime = 0.4f;

	public Camera mainCamera;

	public void ShakeCam()
	{
		cameraInitialPosition = mainCamera.transform.position;
		InvokeRepeating("StartCameraShaking", 0f, 0.005f);
		Invoke("StopCameraShaking", shakeTime);
	}

	private void StartCameraShaking()
	{
		float num = Random.value * shakeMagnitude * 2f - shakeMagnitude;
		float num2 = Random.value * shakeMagnitude * 2f - shakeMagnitude;
		Vector3 position = mainCamera.transform.position;
		position.x += num;
		position.y += num2;
		mainCamera.transform.position = position;
	}

	private void StopCameraShaking()
	{
		CancelInvoke("StartCameraShaking");
		mainCamera.transform.position = cameraInitialPosition;
	}
}
