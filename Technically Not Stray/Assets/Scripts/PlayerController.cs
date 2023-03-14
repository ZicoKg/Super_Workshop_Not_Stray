using UnityEngine;

namespace Cinemachine.Examples
{

    [AddComponentMenu("")] // Don't display in add component menu
    public class CharacterMovement : MonoBehaviour
    {
        public float turnSpeed = 1f;
        public float movementSpeed = 1f;

        private bool useCharacterForward = false;

        private Animator animator;
        private Vector3 targetDirection;
        private Vector2 input;
        private Quaternion freeRotation;
        private Camera mainCamera;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            Cursor.lockState = CursorLockMode.Locked;
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            float translation = input.y * movementSpeed * Time.deltaTime;
            transform.Translate(0, 0, translation);

            if(translation != 0)
            {
                animator.SetBool("isWalking", true);
            } else
            {
                animator.SetBool("isWalking", false);
            }

            // Update target direction relative to the camera view (or not if the Keep Direction option is checked)
            UpdateTargetDirection();
            if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
            {
                Vector3 lookDirection = targetDirection.normalized;
                freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                var eulerY = transform.eulerAngles.y;

                if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * Time.deltaTime);
            }
#else
        InputSystemHelper.EnableBackendsWarningMessage();
#endif
        }

        public virtual void UpdateTargetDirection()
        {
            if (!useCharacterForward)
            {
                var forward = mainCamera.transform.TransformDirection(Vector3.forward);
                forward.y = 0;

                //get the right-facing direction of the referenceTransform
                var right = mainCamera.transform.TransformDirection(Vector3.right);

                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                targetDirection = input.x * right + input.y * forward;
            }
            else
            {
                var forward = transform.TransformDirection(Vector3.forward);
                forward.y = 0;

                //get the right-facing direction of the referenceTransform
                var right = transform.TransformDirection(Vector3.right);
                targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
            }
        }
    }
}
