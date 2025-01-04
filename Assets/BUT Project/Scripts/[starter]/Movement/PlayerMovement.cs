using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace BUT
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        Movement m_Movement;

        float m_CurrentSpeed;
        public float CurrentSpeed
        {
            set
            {
                if (m_CurrentSpeed == value) return;
                m_CurrentSpeed = value;
                OnSpeedChange?.Invoke(m_CurrentSpeed);
            }
            get => m_CurrentSpeed;
        }

        bool m_IsSprinting;
        public bool IsSprinting { set => m_IsSprinting = value; get => m_IsSprinting; }

        private bool m_IsMoving;
        public bool IsMoving
        {
            set
            {
                if (m_IsMoving == value) return;
                m_IsMoving = value;
                OnMovingChange?.Invoke(m_IsMoving);
            }
            get => m_IsMoving;
        }

        private Vector3 m_Direction;
        public Vector3 Direction { set => m_Direction = value; get => m_Direction; }
        public Vector3 FullDirection => (GroundRotationOffset * Direction * CurrentSpeed + Vector3.up * GravityVelocity);

        private Quaternion m_GroundRotationOffset;
        public Quaternion GroundRotationOffset { set => m_GroundRotationOffset = value; get => m_GroundRotationOffset; }

        public const float GRAVITY = -9.31f;

        private float m_GravityVelocity;
        public float GravityVelocity { set => m_GravityVelocity = value; get => m_GravityVelocity; }

        private int m_JumpNumber;
        public int JumpNumber { set => m_JumpNumber = value; get => m_JumpNumber; }

        [SerializeField]
        float m_RayLenght;
        [SerializeField]
        LayerMask m_RayMask;

        RaycastHit m_Hit;

        private bool m_IsGrounded;
        public bool IsGrounded
        {
            set
            {
                if (IsGrounded == value) return;
                m_IsGrounded = value;
                OnGroundedChange?.Invoke(m_IsGrounded);
            }
            get => m_IsGrounded;
        }

        private CharacterController m_CharacterController;
        private Vector2 m_MovementInput;
        private Vector3 m_MovementDirection;

        public UnityEvent<float> OnSpeedChange;
        public UnityEvent<bool> OnMovingChange;
        public UnityEvent<bool> OnGroundedChange;

        // Footstep Audio Variables
        [SerializeField]
        private AudioSource m_AudioSource; // Drag your AudioSource here in the Inspector
        [SerializeField]
        private AudioClip[] m_FootstepSounds; // Array of footstep sounds
        [SerializeField]
        private float m_FootstepInterval = 0.5f; // Time interval between footstep sounds
        private float m_FootstepTimer;

        [SerializeField]
        private ParticleSystem footstepParticles; // Référence au système de particules

        [SerializeField]
        private float particleInterval = 0.5f; // Intervalle entre les particules

        private float particleTimer = 0; // Chronomètre pour les particules


        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            StartCoroutine(Moving());
        }

        IEnumerator Moving()
{
    while (enabled)
    {
        if (m_MovementInput.magnitude > 0.1f)
        {
            if (!IsMoving)
            {
                IsMoving = true;
            }
        }
        else if (IsMoving)
        {
            IsMoving = false;
        }

        if (IsMoving)
        {
            HandleFootsteps(); // Gestion des sons
            HandleFootstepParticles(); // Gestion des particules
        }

        ManageDirection();
        ManageGravity();
        if (IsMoving) ApplyRotation();
        ApplyMovement();
        yield return new WaitForFixedUpdate();
    }
}

        private void HandleFootsteps()
        {
            if (!m_CharacterController.isGrounded || !IsMoving) return;

            m_FootstepTimer += Time.deltaTime;
            if (m_FootstepTimer >= m_FootstepInterval)
            {
                m_FootstepTimer = 0;
                PlayFootstepSound();
            }
        }

        private void PlayFootstepSound()
        {
            if (m_FootstepSounds.Length == 0 || m_AudioSource == null) return;

            int randomIndex = Random.Range(0, m_FootstepSounds.Length);
            m_AudioSource.PlayOneShot(m_FootstepSounds[randomIndex]);
        }

        public void SetInputMove(InputAction.CallbackContext _context)
        {
            m_MovementInput = _context.ReadValue<Vector2>();
        }

        private void ManageDirection()
        {
            m_MovementDirection = new Vector3(m_MovementInput.x, 0, m_MovementInput.y);
            m_MovementDirection = Camera.main.transform.TransformDirection(m_MovementDirection);
            m_MovementDirection.y = transform.forward.y;

            if (Physics.Raycast(transform.position, -transform.up, out m_Hit, m_RayLenght, m_RayMask))
            {
                IsGrounded = true;
                float angleOffset = Vector3.SignedAngle(transform.up, m_Hit.normal, transform.right);
                GroundRotationOffset = Quaternion.AngleAxis(angleOffset, transform.right);
            }
            else
            {
                IsGrounded = m_CharacterController.isGrounded;
                GroundRotationOffset = Quaternion.identity;
            }
            m_MovementDirection.Normalize();

            Direction = m_MovementDirection;
            CurrentSpeed = ((IsSprinting) ? m_Movement.SprintFactor : 1) * m_Movement.MaxSpeed * m_Movement.SpeedFactor.Evaluate(m_MovementInput.magnitude);
        }

        public void ApplyRotation()
        {
            if (!IsMoving) return;

            Quaternion targetRotation = Quaternion.LookRotation(Direction, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
                m_Movement.MaxAngularSpeed * Mathf.Deg2Rad * m_Movement.AngularSpeedFactor.Evaluate(Direction.magnitude) * Time.deltaTime);
        }

        public void ApplyMovement()
        {
            m_CharacterController.Move(FullDirection * Time.deltaTime);
        }

        private void ManageGravity()
        {
            if (m_CharacterController.isGrounded && GravityVelocity < 0.0f)
            {
                GravityVelocity = -1;
            }
            else
            {
                GravityVelocity += GRAVITY * m_Movement.GravityMultiplier * Time.deltaTime;
            }
        }

        private void HandleFootstepParticles()
        {
            if (!m_CharacterController.isGrounded || !IsMoving || footstepParticles == null) return;

                particleTimer += Time.deltaTime;
            if (particleTimer >= particleInterval)
                {
                    particleTimer = 0;
                    EmitFootstepParticles();
                }
        }

        private void EmitFootstepParticles()
        {
            footstepParticles.Play(); // Active le système de particules
        }

    }
}