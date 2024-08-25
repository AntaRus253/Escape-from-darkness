using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    public bool isMovementActive = true;
    public enum MovementAxis { X, Z, Y }
    public MovementAxis movementAxis = MovementAxis.X;
    public float moveDistance = 4f;
    public float moveSpeed = 3.6f;
    public bool movingForward = true;

    private const float ZERO_MOVING = 0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start() {
        startPosition = transform.position;
        SetTargetPosition();
    }

    private void Update() {
        if (isMovementActive) {
            MovePlatform();
        }
    }

    private void MovePlatform() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition) {
            movingForward = !movingForward;
            SetTargetPosition();
        }
    }

    private void SetTargetPosition() {
        if (movementAxis == MovementAxis.X) {
            targetPosition = startPosition + new Vector3(movingForward ? moveDistance : -moveDistance, ZERO_MOVING, ZERO_MOVING);
        }
        else if (movementAxis == MovementAxis.Y) {
            targetPosition = startPosition + new Vector3(ZERO_MOVING, movingForward ? moveDistance : -moveDistance, ZERO_MOVING);
        }
        else if (movementAxis == MovementAxis.Z) {
            targetPosition = startPosition + new Vector3(ZERO_MOVING, ZERO_MOVING, movingForward ? moveDistance : -moveDistance);
        }
    }
}

