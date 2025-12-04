    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI; // Required for Button functionality

    public class CameraMover : MonoBehaviour
    {
        public Camera mainCamera; // Assign your main camera in the Inspector
        public Vector3 targetPosition; // The position the camera will move to
        public float moveSpeed = 5f; // Speed of camera movement

        private Coroutine moveCoroutine; // Track the current movement coroutine

        // This function will be called when the button is clicked
        public void MoveCameraToTarget()
        {
            if (mainCamera != null)
            {
                // Stop any existing movement coroutine
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }

                // Start the camera movement coroutine
                moveCoroutine = StartCoroutine(MoveCameraCoroutine());
            }
        }

        private IEnumerator MoveCameraCoroutine()
        {
            // Continue moving until we're close enough to the target
            while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.01f)
            {
                // Smoothly interpolate towards the target position
                mainCamera.transform.position = Vector3.Lerp(
                    mainCamera.transform.position,
                    targetPosition,
                    Time.deltaTime * moveSpeed
                );

                // Wait for the next frame
                yield return null;
            }

            // Snap to exact target position when close enough
            mainCamera.transform.position = targetPosition;
            moveCoroutine = null;
        }
    }