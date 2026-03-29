using Settings.Input;
using Unity.Cinemachine;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace JES.Code.System
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private float scrollSpeed = 5f;
        [SerializeField] private float scrollMin = 1f;
        [SerializeField] private float scrollMax = 30f;
        [SerializeField] private CinemachinePositionComposer composer;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody == null)
            {
                Debug.LogError("Rigidbody component is missing on the CameraMovement GameObject.");
            }

            inputSO.OnWheelEvent += HandleWheelEvent;
        }

        private void OnDestroy()
        {
            if (inputSO != null)
            {
                inputSO.OnWheelEvent -= HandleWheelEvent;
            }
        }

        private void HandleWheelEvent(float direction)
        {
            if(GameManager.IsOverUI) return;
            
            composer.CameraDistance += -direction * scrollSpeed;
            composer.CameraDistance =Mathf.Clamp(composer.CameraDistance, scrollMin, scrollMax); // Adjust min and max distance as needed
        }

        private void FixedUpdate()
        {
            Vector3 movement = new Vector3(inputSO.MovementInput.x, 0, inputSO.MovementInput.y);
            movement = movement.normalized * moveSpeed;
            _rigidbody.linearVelocity = movement;
        }
    }
}