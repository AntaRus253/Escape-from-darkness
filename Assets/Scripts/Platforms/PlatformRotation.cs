using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    public bool isRotationActive = true;
    public bool isRotateClockwise = true;
    public float rotationSpeed = 2.6f;

    private const float FULL_ROTATION = 360f;
    private const float ZERO_ROTATION = 0f;
    private const float ROTATION_MULTIPLIER = 10f;
    private const float CLOCKWISE_DIRECTION = 1f;
    private const float COUNTER_CLOCKWISE_DIRECTION = -1f;

    private Transform platformRotation;
    private float currentRotation = 0f;

    private void Start() {
        platformRotation = transform;
    }

    private void Update() {
        if (isRotationActive) {
            Rotate();
        }
    }

    private void Rotate() {
        float rotationDirection = isRotateClockwise ? CLOCKWISE_DIRECTION : COUNTER_CLOCKWISE_DIRECTION;

        currentRotation += rotationSpeed * ROTATION_MULTIPLIER * Time.deltaTime * rotationDirection;

        currentRotation = NormalizeRotation(currentRotation);

        platformRotation.rotation = Quaternion.Euler(ZERO_ROTATION, currentRotation, ZERO_ROTATION);
    }

    private float NormalizeRotation(float rotation) {
        rotation %= FULL_ROTATION;
        if (rotation < ZERO_ROTATION) {
            rotation += FULL_ROTATION;
        }
        return rotation;
    }
}
