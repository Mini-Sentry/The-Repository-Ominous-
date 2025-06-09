using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        // DOUBLE JUMP VARIABLES
        private int jumpCount = 0;
        public int maxJumps = 2;  // Allow two jumps

        // DASH VARIABLES
        public float dashSpeed = 15f;
        public float dashTime = 0.2f;
        public float dashdelay = 1.0f;
        private bool isDashing = false;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");

                // DOUBLE JUMP LOGIC
                if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
                {
                    jumpState = JumpState.PrepareToJump;
                    jumpCount++;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }

                // DASH LOGIC
                if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing )
                {
                    StartCoroutine(Dash());
                }
            }
            else
            {
                move.x = 0;
            }

            UpdateJumpState();
            base.Update();
        // Dash cooldown decrease
            if (dashdelay > 0) 
        {
            dashdelay -= (float)0.01;
        } else
        {
            isDashing = false;
        }
        }




        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                        jumpCount = 0; // Reset jump count when landing
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && jumpCount <= maxJumps)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        // DASH FUNCTION
        IEnumerator Dash()
        {
            isDashing = true;
            float originalSpeed = maxSpeed;
            maxSpeed = dashSpeed;
            yield return new WaitForSeconds(dashTime);
            maxSpeed = originalSpeed;
            
            dashdelay += 1f; //resets cooldown
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}
